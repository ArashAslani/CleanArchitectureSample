using UM.Domain.UserAgg;

namespace UM.Application.Users.AddToken;

public class AddUserTokenCommand : IBaseCommand
{
    public AddUserTokenCommand(UserId userId, string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        UserId = userId;
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public UserId UserId { get; }
    public string HashJwtToken { get; }
    public string HashRefreshToken { get; }
    public DateTime TokenExpireDate { get; }
    public DateTime RefreshTokenExpireDate { get; }
    public string Device { get; }

}