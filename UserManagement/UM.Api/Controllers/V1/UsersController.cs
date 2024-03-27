using Common.DotNetCore;
using Common.DotNetCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Security;
using UM.Api.ViewModels.Users;
using UM.Application.Users.AddUserRole;
using UM.Application.Users.ChangePassword;
using UM.Application.Users.Create;
using UM.Application.Users.Edit;
using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;
using UM.Domain.Users;
using UM.Query.Users.DTOs;
using UM.ServiceHost.Facade.Users;

namespace UM.Api.Controllers.V1;

[ApiVersion("1")]
[Authorize]
public class UsersController : ApiController
{
    private readonly IUserFacade _userFacade;
    public UsersController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [PermissionControl(Permission.User_Management)]
    [HttpGet]
    public virtual async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("Current")]
    public virtual async Task<ApiResult<UserDto>> GetCurrentUser()
    {
        var userId = User.GetUserId().ToUserIdInstance();
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpGet("{userIdStr}")]
    public virtual async Task<ApiResult<UserDto?>> GetById(string userIdStr)
    {
        var userId = Guid.Parse(userIdStr).ToUserIdInstance();
        var result = await _userFacade.GetUserById(userId);
        return QueryResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpPost]
    public virtual async Task<ApiResult> Create(CreateUserCommand command)
    {
        var result = await _userFacade.CreateUser(command);
        return CommandResult(result);
    }

    [HttpPost("SetUserRoles")]
    public virtual async Task<ApiResult> SetUserRoles(AddUserRolesViewModel command)
    {
        var userRoles = new List<UserRole>();
        foreach (var roleId in command.Roles)
        {
            userRoles.Add(new UserRole(roleId.ToRoleIdInstance()));
        }
        var addUserRolesCommand = new AddUserRoleCommand()
        {
            UserId = command.UserId.ToUserIdInstance(),
            UserRoles = userRoles
        };
        var result = await _userFacade.AddUserRoles(addUserRolesCommand);
        return CommandResult(result);
    }

    [HttpPut("ChangePassword")]
    public virtual async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
    {

        var changePasswordModel = new ChangeUserPasswordCommand()
        {
            UserId = User.GetUserId().ToUserIdInstance(),
            CurrentPassword = command.CurrentPassword,
            Password = command.Password
        };
        var result = await _userFacade.ChangePassword(changePasswordModel);
        return CommandResult(result);
    }

    [HttpPut("Current")]
    public virtual async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var userId = User.GetUserId().ToUserIdInstance();
        var commandModel = new EditUserCommand(userId, command.Avatar, command.Name, command.Family,
            command.PhoneNumber, command.Email, command.Gender);

        var result = await _userFacade.EditUser(commandModel);
        return CommandResult(result);
    }

    [PermissionControl(Permission.User_Management)]
    [HttpPut]
    public virtual async Task<ApiResult> Edit([FromForm] EditUserCommand command)
    {
        var result = await _userFacade.EditUser(command);
        return CommandResult(result);
    }
}