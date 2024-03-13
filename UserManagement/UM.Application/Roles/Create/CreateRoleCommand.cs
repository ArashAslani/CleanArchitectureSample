using UM.Domain.RoleAgg.Enums;

namespace UM.Application.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;