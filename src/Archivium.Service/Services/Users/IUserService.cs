using Archivium.Domain.Entities.Users;
using Archivium.Service.Configurations;

namespace Archivium.Service.Services.Users;

public interface IUserService
{
    ValueTask<User> CreateUserAsync(User user);
    ValueTask<User> CreateAdminAsync(User user);
    ValueTask<User> UpdateAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> GetByIdAsync(long id);
    ValueTask<bool> RemoveAdminRoleAsync();
    ValueTask<bool> SendCodeAsync(string phone);
    ValueTask<User> ChangeUserStatusAsync(long id);
    ValueTask<bool> ConfirmCodeAsync(string phone, string code);
    ValueTask<bool> ResetPasswordAsync(string phone, string newPassword);
    ValueTask<(User user, string token)> LoginAsync(string phone, string password);
    ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword);
    ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
