﻿using Archivium.WebApi.ApiServices.Accounts;
using Archivium.WebApi.Models.Accounts;
using Archivium.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Archivium.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountApiService accountApiService) : ControllerBase
{
    [HttpGet("login")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> LoginAsync([FromQuery] LoginModel loginModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.LoginAsync(loginModel)
        });
    }

    [HttpGet("send-code")]
    public async ValueTask<IActionResult> SendCodeAsync([FromQuery] SendCodeModel sendCodeModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.SendCodeAsync(sendCodeModel)
        });
    }

    [HttpGet("confirm-code")]
    public async ValueTask<IActionResult> ConfirmAsync([FromQuery] ConfirmCodeModel confirmCodeModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.ConfirmCodeAsync(confirmCodeModel)
        });
    }

    [HttpPatch("reset-password")]
    public async ValueTask<IActionResult> ResetPasswordAsync([FromQuery] ResetPasswordModel resetPasswordModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.ResetPasswordAsync(resetPasswordModel)
        });
    }
}