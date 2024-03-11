using Common.Domain;

namespace UM.Domain.UserAgg;

public class UserRole : BaseEntity<long>
{
    public UserRole(long roleId)
    {
        RoleId = roleId;
    }

    public UserId UserId { get; internal set; }
    public long RoleId { get; private set; }
}
