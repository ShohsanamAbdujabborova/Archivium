using Archivium.Domain.Entities.Enums;
using Archivium.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Archivium.WebApi.Models.Commons;

namespace Archivium.WebApi.Services;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public CustomAuthorize(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

        var allowAnonymous = actionDescriptor?.MethodInfo.GetCustomAttributes(inherit: true)
                .OfType<AllowAnonymousAttribute>().Any() ?? false;
        if (allowAnonymous) return;

        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated || !_roles.Any(user.IsInRole))
        {
            SetStatusCodeResult(context);
            return;
        }
    }

    private void SetStatusCodeResult(AuthorizationFilterContext context)
    {
        var exception = new CustomException("You do not have permission for this method", 403);
        context.Result = new ObjectResult(new Response
        {
            StatusCode = exception.StatusCode,
            Message = exception.Message
        })
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    public bool IsUserAdmin(ClaimsPrincipal user)
    {
        var roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        return user.Claims.Any(c => c.Type == roleClaimType && c.Value == nameof(UserRole.Admin));
    }
}
