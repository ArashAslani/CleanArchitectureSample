using Common.Domain;
using UM.Domain.RoleAgg;

namespace UM.Domain.UserAgg;

public class UserRole : BaseEntity<long>
{
    public UserRole(RoleId roleId)
    {
        RoleId = roleId;
    }

    public UserId UserId { get; internal set; }
    public RoleId RoleId { get; private set; }
}
