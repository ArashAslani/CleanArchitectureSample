using Common.Query;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;