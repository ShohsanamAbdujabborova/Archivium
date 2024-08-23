using Archivium.Domain.Entities.Enums;
using Archivium.WebApi.ApiServices.Likes;
using Archivium.WebApi.Models.Commons;
using Archivium.WebApi.Models.Likes;
using Archivium.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archivium.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Admin), nameof(UserRole.User))]
public class LikesController(ILikeApiService fieldApiService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(LikeCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldApiService.PostAsync(createModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldApiService.DeleteAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet("user/{userId:long}")]
    public async ValueTask<IActionResult> GetAllByUserIdAsync(long userId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldApiService.GetAllAByUserIdsync(userId)
        });
    }

    [AllowAnonymous]
    [HttpGet("item/{itemId:long}")]
    public async ValueTask<IActionResult> GetAllByItemIdAsync(long itemId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldApiService.GetAllAByItemIdsync(itemId)
        });
    }
}