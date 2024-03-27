using Common.Application.SecurityUtilities;
using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Jwt;
using UM.Api.ViewModels.Auth;
using UM.Application.Users.AddToken;
using UM.Application;
using UM.Query.Users.DTOs;
using UAParser;
using Common.Domain.ValueObjects;
using UM.Application.Users.Register;
using UM.Application.Users.RemoveToken;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using UM.ServiceHost.Facade.Users;

namespace UM.Api.Controllers.V1;

[ApiVersion("1")]
public class AuthController : ApiController
{
    private readonly IConfiguration _configuration;
    private readonly IUserFacade _userFacade;
    private readonly IJwtService _jwtService;
    public AuthController(IConfiguration configuration, IUserFacade userFacade, IJwtService jwtService)
    {
        _configuration = configuration;
        _userFacade = userFacade;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public virtual async Task<ActionResult?> Login([FromForm]LoginViewModel loginViewModel)
    {
        var user = await _userFacade.GetUserByPhoneNumber(loginViewModel.UserName);
        if (user == null)
        {
            return new JsonResult(new { IsSuccess = false, StatusCode = HttpStatusCode.NotFound, Message = "UserName Or Password Is Incurrect." });
        }

        if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
        {
            return new JsonResult(new { IsSuccess = false, StatusCode = HttpStatusCode.NotFound, Message = "UserName Or Password Is Incurrect." });
        }

        if (user.IsActive == false)
        {
            return new JsonResult(new { IsSuccess = false, StatusCode = HttpStatusCode.Forbidden, Message = "Your account is deactivate." });
        }

        var jwtIperation = await _jwtService.GenerateAsync(user);
        return new JsonResult(jwtIperation.Data);
    }

    [HttpPost("register")]
    public virtual async Task<ApiResult> Register(RegisterViewModel register)
    {
        var command = new RegisterUserCommand(new PhoneNumber(register.PhoneNumber), register.Password);
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }

    [HttpPost("RefreshToken")]
    public virtual async Task<ActionResult> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);

        if (result == null)
            return new JsonResult((IsSuccess: false, StatusCode: HttpStatusCode.NotFound, Message: "User not found."));


        if (result.TokenExpireDate > DateTime.Now)
        {
            return new JsonResult((IsSuccess: false, StatusCode: HttpStatusCode.OK, Message: "The token has not expired yet."));
        }

        if (result.RefreshTokenExpireDate < DateTime.Now)
        {
            return new JsonResult((IsSuccess: false, StatusCode: HttpStatusCode.NotAcceptable, Message: "Token refresh time is over."));
        }

        var user = await _userFacade.GetUserById(result.UserId);

        var removeTokenResult = await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));

        if (removeTokenResult.Status != OperationResultStatus.Success)
            return new JsonResult((IsSuccess: false, StatusCode: HttpStatusCode.InternalServerError, Message: removeTokenResult.Message));


        var jwt = await _jwtService.GenerateAsync(user);

        return new JsonResult(jwt);
    }


    [Authorize]
    [HttpDelete("logout")]
    public virtual async Task<ApiResult> Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var result = await _userFacade.GetUserTokenByJwtToken(token);
        if (result == null)
            return CommandResult(OperationResult.NotFound());

        var removeTokenResult = await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));

        return CommandResult(removeTokenResult);
    }
}
