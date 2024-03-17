using UM.Domain.RoleAgg.Enums;

namespace UM.Api.ViewModels.Users;

public class AddUserRolesViewModel
{
    public Guid UserId { get; set; }
    public List<Guid> Roles { get; set; }
}
