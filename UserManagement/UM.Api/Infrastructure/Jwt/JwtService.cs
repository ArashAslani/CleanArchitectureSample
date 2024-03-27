using Common.Application.SecurityUtilities;
using Common.DotNetCore.Utilities.DependencyInjection;
using MediatR;
using UAParser;
using UM.Application;
using UM.Application.Users.AddToken;
using UM.Query.Users.DTOs;

namespace UM.Api.Infrastructure.Jwt;


public class JwtService : IJwtService, IScopedDependency
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public JwtService(IHttpContextAccessor httpContextAccessor, IMediator mediator, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        _configuration = configuration;
    }

    public async Task<OperationResult<AccessToken?>> GenerateAsync(UserDto user)
    {
        var uaParser = Parser.GetDefault();
        var header = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();
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

