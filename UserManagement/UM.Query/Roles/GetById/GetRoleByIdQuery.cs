using Common.Query;
using UM.Domain.RoleAgg;
using UM.Query.Roles.DTOs;

namespace UM.Query.Roles.GetById;

public record GetRoleByIdQuery(RoleId RoleId) : IQuery<RoleDto?>;