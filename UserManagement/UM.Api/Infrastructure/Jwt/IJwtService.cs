using UM.Application;
using UM.Domain.Users;
using UM.Query.Users.DTOs;

namespace UM.Api.Infrastructure.Jwt;

public interface IJwtService
{
    Task<OperationResult<AccessToken>> GenerateAsync(UserDto user);
}

