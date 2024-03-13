using Common.Application.SecurityUtilities;
using Common.DotNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UM.Domain.UserAgg;
using UM.Query.Users.GetById;
using UM.Query.Users.UserTokens.GetByJwtToken;

namespace UM.Api.Infrastructure.Jwt;

public class CustomJwtValidation
{
    private readonly IMediator _mediator;

    public CustomJwtValidation(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var userId = new UserId(context.Principal.GetUserId());
        var jwtToken = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        var hashJwtToken = Sha256Hasher.Hash(jwtToken);

        var token = await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
        if (token == null)
        {
            context.Fail("Token NotFound");
            return;
        }


        var user = await _mediator.Send(new GetUserByIdQuery(userId)); ;
        if (user == null || user.IsActive == false)
        {
            context.Fail("User InActive");
            return;
        }
    }
}