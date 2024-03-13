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

namespace UM.Api.Controllers
{
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
        public async Task<ApiResult<LoginResultDto?>> Login(LoginViewModel loginViewModel)
        {
            var user = await _mediator.Send(new GetUserByPhoneNumberQuery(loginViewModel.PhoneNumber));
            if (user == null)
            {
                var result = OperationResult<LoginResultDto>.Error("User not found.");
                return CommandResult(result);
            }

            if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
            {
                var result = OperationResult<LoginResultDto>.Error("User not found.");
                return CommandResult(result);
            }

            if (user.IsActive == false)
            {
                var result = OperationResult<LoginResultDto>.Error("Your account is deactivate.");
                return CommandResult(result);
            }

            var loginResult = await AddTokenAndGenerateJwt(user);
            return CommandResult(loginResult);
        }

        [HttpPost("register")]
        public async Task<ApiResult> Register(RegisterViewModel register)
        {
            var command = new RegisterUserCommand(new PhoneNumber(register.PhoneNumber), register.Password);
            var result = await _mediator.Send(command);
            return CommandResult(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
        {
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            var result = await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));

            if (result == null)
                return CommandResult(OperationResult<LoginResultDto?>.NotFound());

            if (result.TokenExpireDate > DateTime.Now)
            {
                return CommandResult(OperationResult<LoginResultDto>.Error("The token has not expired yet."));
            }

            if (result.RefreshTokenExpireDate < DateTime.Now)
            {
                return CommandResult(OperationResult<LoginResultDto>.Error("Token refresh time is over."));
            }

            var user = await _mediator.Send(new GetUserByIdQuery(result.UserId));

            var removeTokenResult = await _mediator.Send(new RemoveUserTokenCommand(result.UserId, result.Id));

            if (removeTokenResult.Status != OperationResultStatus.Success)
                return CommandResult(OperationResult<LoginResultDto>.Error(removeTokenResult.Message));

            var loginResult = await AddTokenAndGenerateJwt(user);

            return CommandResult(loginResult);
        }


        [Authorize]
        [HttpDelete("logout")]
        public async Task<ApiResult> Logout()
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

        private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
        {
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers.UserAgent.ToString();
            var device = "windows";
            if (header != null)
            {
                var info = uaParser.Parse(header);
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            var refreshToken = Guid.NewGuid().ToString();

            var hashJwt = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

            var tokenResult = await _mediator.Send(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));
            if (tokenResult.Status != OperationResultStatus.Success)
                return OperationResult<LoginResultDto?>.Error();

            return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }
    }
}
