using Common.Query;
using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;

namespace UM.Query.Roles.DTOs;

public class RoleDto : BaseDto<RoleId>
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}