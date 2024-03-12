using Common.Domain.Exceptions;
using Common.Domain;

namespace UM.Domain.UserAgg;

public class UserToken : BaseEntity<long>
{
    private UserToken()
    {

    }
    public UserToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        Guard(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public UserId UserId { get; internal set; }
    public string HashJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }


    public void Guard(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        NullOrEmptyDomainDataException.CheckString(hashJwtToken, nameof(HashJwtToken));
        NullOrEmptyDomainDataException.CheckString(hashRefreshToken, nameof(HashRefreshToken));

        if (tokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Invalid Token ExpireDate");

        if (refreshTokenExpireDate < tokenExpireDate)
            throw new InvalidDomainDataException("Invalid RefreshToken ExpireDate");
    }
}