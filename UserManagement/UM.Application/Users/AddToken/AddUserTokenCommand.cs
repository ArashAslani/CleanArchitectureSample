
using UM.Domain.UserAgg;

namespace UM.Application.Users.AddToken;

public class AddUserTokenCommand(UserId userId, string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device) : IBaseCommand
{
    public UserId UserId { get; } = userId;
    public string HashJwtToken { get; } = hashJwtToken;
    public string HashRefreshToken { get; } = hashRefreshToken;
    public DateTime TokenExpireDate { get; } = tokenExpireDate;
    public DateTime RefreshTokenExpireDate { get; } = refreshTokenExpireDate;
    public string Device { get; } = device;

}