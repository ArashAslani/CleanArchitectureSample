using Common.DotNetCore;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Security;
using UM.Application.Roles.Create;
using UM.Application.Roles.Edit;
using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;
using UM.Query.Roles.DTOs;
using UM.ServiceHost.Facade.Roles;

namespace UM.Api.Controllers.V1;


[ApiVersion("1")]
[PermissionControl(Permission.Role_Management)]
public class RoleController : ApiController
{
    private readonly IRoleFacade _roleFacade;

    public RoleController(IRoleFacade roleFacade)
    {
        _roleFacade = roleFacade;
    }

    [HttpGet]
    public virtual async Task<ApiResult<List<RoleDto>>> GetRoles()
    {
        var result = await _roleFacade.GetRoles();
        return QueryResult(result);
    }

    [HttpGet("{roleIdStr}")]
    public virtual async Task<ApiResult<RoleDto?>> GetRoleById(string roleIdStr)
    {
        var roleId = roleIdStr.ToRoleIdInstance();
        var result = await _roleFacade.GetRoleById(roleId);
        return QueryResult(result);
    }

    [HttpPost]
    public virtual async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await _roleFacade.CreateRole(command);
        return CommandResult(result);
    }

    [HttpPut]
    public virtual async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        var result = await _roleFacade.EditRole(command);
        return CommandResult(result);
    }
}