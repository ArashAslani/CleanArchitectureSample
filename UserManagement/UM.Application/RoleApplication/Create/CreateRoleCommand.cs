using UM.Domain.RoleAgg.Enums;

namespace UM.Application.RoleApplication.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;