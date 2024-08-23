using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Users;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace Archivium.Service.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IUserService
{
    private readonly string cacheKey = "EmailCodeKey";

    private async Task<User> CreateUserAsync(User user, Domain.Enums.UserRole role)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Phone == user.Phone || u.Email == user.Email);
        if (existUser is not null)
            throw new AlreadyExistException($"This user already exists with this phone or email");

        user.UserRole = role;
        user.CreatedByUserId = HttpContextHelper.UserId;
        user.PasswordHash = PasswordHasher.Hash(user.PasswordHash);

        var createdUser = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();
        return createdUser;
    }

    public async ValueTask<User> CreateUserAsync(User user)
    {
        return await CreateUserAsync(user, Domain.Enums.UserRole.User);
    }

    public async ValueTask<User> CreateAdminAsync(User user)
    {
        return await CreateUserAsync(user, Domain.Enums.UserRole.Admin);
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => (u.Phone == user.Phone || u.Email == user.Email) && u.Id != id);
        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"This user already exists with this phone={user.Phone}");

        existUser.Phone = user.Phone;
        existUser.Email = user.Email;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        existUser.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<User> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id, includes: ["Collections", "Likes", "Comments"])
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        return existUser;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(includes: ["Collections", "Likes", "Comments"], isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(user =>
                user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                user.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await users.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<(User user, string token)> LoginAsync(string phone, string password)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Phone == phone)
            ?? throw new ArgumentIsNotValidException($"Phone or password is not valid");

        if (!PasswordHasher.Verify(password, existUser.PasswordHash))
            throw new ArgumentIsNotValidException($"Phone or password is not valid");

        return (user: existUser, token: AuthHelper.GenerateToken(existUser));
    }

    public async ValueTask<bool> ResetPasswordAsync(string phone, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Phone == phone && !user.IsDeleted)
            ?? throw new NotFoundException($"User is not found with this phone={phone}");

        var code = memoryCache.Get(cacheKey) as string;
        if (!await ConfirmCodeAsync(phone, code))
            throw new ArgumentIsNotValidException("Confirmation failed");

        existUser.PasswordHash = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> SendCodeAsync(string phone)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Phone == phone)
            ?? throw new NotFoundException($"User is not found with this phone={phone}");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(user.Email, "Confirmation Code", code.ToString());

        var memoryCacheOptions = new MemoryCacheEntryOptions()
             .SetSize(50)
             .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
             .SetSlidingExpiration(TimeSpan.FromSeconds(30))
             .SetPriority(CacheItemPriority.Normal);

        memoryCache.Set(cacheKey, code.ToString(), memoryCacheOptions);

        return true;
    }

    public async ValueTask<bool> ConfirmCodeAsync(string phone, string code)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Phone == phone)
            ?? throw new NotFoundException($"User is not found with this phone={phone}");

        if (memoryCache.Get(cacheKey) as string == code)
            return true;

        return false;
    }

    public async ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Phone == phone && PasswordHasher.Verify(oldPassword, u.PasswordHash))
            ?? throw new ArgumentIsNotValidException($"Phone or password is not valid");

        existUser.PasswordHash = PasswordHasher.Hash(newPassword);
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }

    public async ValueTask<bool> RemoveAdminRoleAsync()
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == HttpContextHelper.UserId);
        existUser.UserRole = Domain.Enums.UserRole.User;
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<User> ChangeUserStatusAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        existUser.IsBlocked = !existUser.IsBlocked;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }
}