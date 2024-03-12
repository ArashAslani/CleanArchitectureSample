using Common.Query;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;