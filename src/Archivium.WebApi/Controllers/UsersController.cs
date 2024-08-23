using Archivium.Domain.Entities.Enums;
using Archivium.Service.Configurations;
using Archivium.WebApi.ApiServices.Users;
using Archivium.WebApi.Models.Commons;
using Archivium.WebApi.Models.Users;
using Archivium.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archivium.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Admin), nameof(UserRole.User))]
public class UsersController(IUserApiService userApiService) : BaseController
{
    [AllowAnonymous]
    [HttpPost("admin")]
    public async ValueTask<IActionResult> PostAdminAsync(UserCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.PostAdminAsync(createModel)
        });
    }

    [AllowAnonymous]
    [HttpPost("user")]
    public async ValueTask<IActionResult> PostAsync(UserCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.PostUserAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, UserUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.PutAsync(id, updateModel)
        });
    }

    [CustomAuthorize(nameof(UserRole.Admin))]
    [HttpPut("changeBlockStatus/{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.ChangeUserStatusAsync(id)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.GetAsync(id)
        });
    }

    [HttpGet]
    [AllowAnonymous]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.GetAsync(@params, filter, search)
        });
    }

    [HttpPatch("change-password")]
    public async ValueTask<IActionResult> ChangePasswordAsync([FromQuery] UserChangePasswordModel userChangePasswordModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.ChangePasswordAsync(userChangePasswordModel)
        });
    }

    [CustomAuthorize(nameof(UserRole.Admin))]
    [HttpPatch("remove-adminRole")]
    public async ValueTask<IActionResult> RemoveAdminRoleAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.RemoveAdminRoleAsync()
        });
    }
}