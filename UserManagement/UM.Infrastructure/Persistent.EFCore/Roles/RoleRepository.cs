using UM.Domain.RoleAgg;
using UM.Infrastructure.Utilities;
using UM.Domain.RoleAgg.Repository;

namespace UM.Infrastructure.Persistent.EFCore.RoleAgg;

internal class RoleRepository : BaseRepository<Role, RoleId>, IRoleRepository
{
    public RoleRepository(UserManagementContext context) : base(context)
    {
    }
}