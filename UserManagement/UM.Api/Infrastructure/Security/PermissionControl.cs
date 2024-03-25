using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UM.Domain.RoleAgg.Enums;
using UM.Domain.Users;
using UM.Query.Roles.GetList;
using UM.Query.Users.GetById;

namespace UM.Api.Infrastructure.Security;

public class PermissionControl : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IMediator _mediator;
    private readonly Permission _permission;

    public PermissionControl(Permission permission)
    {
        _permission = permission;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
            return;

        _mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorize");
        }
    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }

        return hasAllowAnonymous;
    }
    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var userId = new UserId(context.HttpContext.User.GetUserId());
        var user = await _mediator.Send(new GetUserByIdQuery(userId));
        if (user == null)
            return false;

        var roleIds = user.Roles.Select(s => s.RoleId).ToList();
        var roles = await _mediator.Send(new GetRoleListQuery());
        var userRoles = roles.Where(r => roleIds.Contains(r.Id));

        return userRoles.Any(r => r.Permissions.Contains(_permission));
    }
}