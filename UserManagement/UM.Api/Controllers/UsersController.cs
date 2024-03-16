using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Security;
using UM.Api.ViewModels.Users;
using UM.Application.Users.ChangePassword;
using UM.Application.Users.Create;
using UM.Application.Users.Edit;
using UM.Domain.RoleAgg.Enums;
using UM.Domain.UserAgg;
using UM.Query.Users.DTOs;
using UM.Query.Users.GetByFilter;
using UM.Query.Users.GetById;

namespace Shop.Api.Controllers;


[Authorize]
public class UsersController : ApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [PermissionControl(Permission.User_Management)]
    [HttpGet]
    public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
    {
        var result = await _mediator.Send(new GetUserByFilterQuery(filterParams));
        return QueryResult(result);
    }

    [HttpGet("Current")]
    public async Task<ApiResult<UserDto>> GetCurrentUser()
    {
        var userId = new UserId(User.GetUserId());
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return QueryResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDto?>> GetById(UserId userId)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return QueryResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpPost]
    public async Task<ApiResult> Create(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CommandResult(result);
    }

    [HttpPut("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
    {

        var changePasswordModel = new ChangeUserPasswordCommand()
        {
            UserId = new UserId (User.GetUserId()),
            CurrentPassword = command.CurrentPassword,
            Password = command.Password
        };
        var result = await _mediator.Send(changePasswordModel);
        return CommandResult(result);
    }

    [HttpPut("Current")]
    public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var userId = new UserId(User.GetUserId());
        var commandModel = new EditUserCommand(userId, command.Avatar, command.Name, command.Family,
            command.PhoneNumber, command.Email, command.Gender);

        var result = await _mediator.Send(commandModel);
        return CommandResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CommandResult(result);
    }
}