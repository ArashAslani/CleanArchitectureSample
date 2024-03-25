using Common.Application.SecurityUtilities;
using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UM.Api.Infrastructure.Jwt;
using UM.Api.ViewModels.Auth;
using UM.Application.Users.AddToken;
using UM.Application;
using UM.Query.Users.DTOs;
using UM.Query.Users.GetByPhoneNumber;
using UAParser;
using Common.Domain.ValueObjects;
using UM.Application.Users.Register;
using UM.Application.Users.RemoveToken;
using UM.Query.Users.UserTokens.GetByRefreshToken;
using UM.Query.Users.GetById;
using Microsoft.AspNetCore.Authentication;
using UM.Query.Users.UserTokens.GetByJwtToken;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace UM.Api.Controllers.V1;

[ApiVersion("1")]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    public AuthController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }
    [HttpPost("login")]
    public virtual async Task<ActionResult?> Login([FromForm]LoginViewModel loginViewModel)
    {
        var user = await _mediator.Send(new GetUserByPhoneNumberQuery(loginViewModel.UserName));
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

        var jwtIperation = await AddTokenAndGenerateJwt(user);
        return new JsonResult(jwtIperation.Data);
    }

    [HttpPost("register")]
    public virtual async Task<ApiResult> Register(RegisterViewModel register)
    {
        var command = new RegisterUserCommand(new PhoneNumber(register.PhoneNumber), register.Password);
        var result = await _mediator.Send(command);
        return CommandResult(result);
    }

    [HttpPost("RefreshToken")]
    public virtual async Task<ActionResult> RefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        var result = await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));

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

        var user = await _mediator.Send(new GetUserByIdQuery(result.UserId));

        var removeTokenResult = await _mediator.Send(new RemoveUserTokenCommand(result.UserId, result.Id));

        if (removeTokenResult.Status != OperationResultStatus.Success)
            return new JsonResult((IsSuccess: false, StatusCode: HttpStatusCode.InternalServerError, Message: removeTokenResult.Message));


        var jwt = await AddTokenAndGenerateJwt(user);

        return new JsonResult(jwt);
    }


    [Authorize]
    [HttpDelete("logout")]
    public virtual async Task<ApiResult> Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var hashJwtToken = Sha256Hasher.Hash(token);
        var result = await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
        if (result == null)
            return CommandResult(OperationResult.NotFound());

        var removeTokenResult = await _mediator.Send(new RemoveUserTokenCommand(result.UserId, result.Id));

        if (removeTokenResult.Status != OperationResultStatus.Success)
            return CommandResult(OperationResult.Error(removeTokenResult.Message));


        return CommandResult(OperationResult.Success());
    }

    private virtual async Task<OperationResult<AccessToken?>> AddTokenAndGenerateJwt(UserDto user)
    {
        var uaParser = Parser.GetDefault();
        var header = HttpContext.Request.Headers.UserAgent.ToString();
        var device = "windows";
        if (header != null)
        {
            var info = uaParser.Parse(header);
            device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        }

        var jwtSecurityToken = JwtTokenBuilder.BuildToken(user, _configuration);
        var accessToken = new AccessToken(jwtSecurityToken);

        var hashedAccessToken = Sha256Hasher.Hash(accessToken.access_token);
        var hashedRefreshToken = Sha256Hasher.Hash(accessToken.refresh_token);

        var tokenResult = await _mediator.Send(new AddUserTokenCommand(user.Id, hashedAccessToken, hashedRefreshToken, DateTime.Now.AddSeconds(accessToken.expires_in), DateTime.Now.AddDays(1), device));
        if (tokenResult.Status != OperationResultStatus.Success)
            return OperationResult<AccessToken?>.Error();

        return OperationResult<AccessToken?>.Success(accessToken);
    }
}
