using Common.Domain;
using UM.Domain.RoleAgg.Enums;

namespace UM.Domain.RoleAgg;

public class RolePermission : BaseEntity<long>
{
    public RolePermission(Permission permission)
    {
        Permission = permission;
    }

    public RoleId RoleId { get; internal set; }
    public Permission Permission { get; private set; }
}