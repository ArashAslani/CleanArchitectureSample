using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UM.Api.Infrastructure.Jwt;

public interface ICustomJwtValidation
{
    Task Validate(TokenValidatedContext context);
}
