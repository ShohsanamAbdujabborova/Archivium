using Archivium.WebApi.Models.Accounts;
using Archivium.WebApi.Models.Users;

namespace Archivium.WebApi.ApiServices.Accounts;

public interface IAccountApiService
{
    ValueTask<UserLoginViewModel> LoginAsync(LoginModel loginModel);
    ValueTask<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    ValueTask<bool> SendCodeAsync(SendCodeModel sendCodeModel);
    ValueTask<bool> ConfirmCodeAsync(ConfirmCodeModel confirmCodeModel);
}