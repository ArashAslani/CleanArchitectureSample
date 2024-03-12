using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;

namespace UM.Application.RoleApplication.Edit;

public record EditRoleCommand(RoleId Id, string Title, List<Permission> Permissions) : IBaseCommand;