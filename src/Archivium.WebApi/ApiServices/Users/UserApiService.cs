using Archivium.Domain.Entities.Users;
using Archivium.Service.Configurations;
using Archivium.Service.Services.Users;
using Archivium.WebApi.Extensions;
using Archivium.WebApi.Models.Users;
using Archivium.WebApi.Validators.Users;
using AutoMapper;

namespace Archivium.WebApi.ApiServices.Users;

public class UserApiService(
    IMapper mapper,
    IUserService userService,
    UserCreateModelValidator createModelValidator,
    UserUpdateModelValidator updateModelValidator,
    UserChangePasswordModelValidator changePasswordValidator) : IUserApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userService.DeleteAsync(id);
    }

    public async ValueTask<UserViewModel> GetAsync(long id)
    {
        var user = await userService.GetByIdAsync(id);
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = await userService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    public async ValueTask<UserViewModel> PostAdminAsync(UserCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var createdUser = await userService.CreateAdminAsync(mapper.Map<User>(createModel));
        return mapper.Map<UserViewModel>(createdUser);
    }

    public async ValueTask<UserViewModel> PostUserAsync(UserCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var createdUser = await userService.CreateUserAsync(mapper.Map<User>(createModel));
        return mapper.Map<UserViewModel>(createdUser);
    }

    public async ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var updatedUser = await userService.UpdateAsync(id, mapper.Map<User>(updateModel));
        return mapper.Map<UserViewModel>(updatedUser);
    }

    public async ValueTask<UserViewModel> ChangePasswordAsync(UserChangePasswordModel changePasswordModel)
    {
        await changePasswordValidator.EnsureValidatedAsync(changePasswordModel);
        var user = await userService
            .ChangePasswordAsync(changePasswordModel.Phone, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<UserViewModel> ChangeUserStatusAsync(long id)
    {
        var user = await userService.ChangeUserStatusAsync(id);
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<bool> RemoveAdminRoleAsync()
    {
        return await userService.RemoveAdminRoleAsync();
    }
}
