using Common.Query;
using UM.Domain.UserAgg;

namespace UM.Query.Users.DTOs;

public class UserTokenDto : BaseDto<long>
{
    public UserId UserId { get; set; }
    public string HashJwtToken { get; set; }
    public string HashRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }
}