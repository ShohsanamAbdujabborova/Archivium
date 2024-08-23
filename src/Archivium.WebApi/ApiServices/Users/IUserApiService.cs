using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Users;

namespace Archivium.WebApi.ApiServices.Users;

public interface IUserApiService
{
    ValueTask<UserViewModel> PostUserAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> PostAdminAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel createModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<UserViewModel> ChangePasswordAsync(UserChangePasswordModel changePasswordModel);
    ValueTask<UserViewModel> ChangeUserStatusAsync(long id);
    ValueTask<bool> RemoveAdminRoleAsync();
}