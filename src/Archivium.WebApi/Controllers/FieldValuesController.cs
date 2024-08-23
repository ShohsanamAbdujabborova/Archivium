using Archivium.Domain.Entities.Enums;
using Archivium.Service.Configurations;
using Archivium.WebApi.ApiServices.FieldValues;
using Archivium.WebApi.Models.Commons;
using Archivium.WebApi.Models.FieldValues;
using Archivium.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archivium.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Admin), nameof(UserRole.User))]
public class FieldValuesController(IFieldValueApiService fieldValueApiService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(FieldValueCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldValueApiService.PostAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, FieldValueUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldValueApiService.PutAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldValueApiService.DeleteAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldValueApiService.GetAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await fieldValueApiService.GetAsync(@params, filter, search)
        });
    }
}
