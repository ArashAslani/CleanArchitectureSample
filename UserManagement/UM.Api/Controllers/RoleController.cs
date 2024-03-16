using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Security;
using UM.Application.Roles.Create;
using UM.Application.Roles.Edit;
using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;
using UM.Query.Roles.DTOs;
using UM.Query.Roles.GetById;
using UM.Query.Roles.GetList;

namespace UM.Api.Controllers;


[PermissionControl(Permission.Role_Management)]
public class RoleController : ApiController
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResult<List<RoleDto>>> GetRoles()
    {
        var result = await _mediator.Send(new GetRoleListQuery()); ;
        return QueryResult(result);
    }

    [HttpGet("{roleId}")]
    public async Task<ApiResult<RoleDto?>> GetRoleById(RoleId roleId)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(roleId));
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return CommandResult(result);
    }
}