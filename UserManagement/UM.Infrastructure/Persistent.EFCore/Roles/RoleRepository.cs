using UM.Domain.RoleAgg;
using UM.Infrastructure.Utilities;
using UM.Domain.RoleAgg.Repository;
using Common.DotNetCore.Utilities.DependencyInjection;

namespace UM.Infrastructure.Persistent.EFCore.RoleAgg;

internal class RoleRepository : BaseRepository<Role, RoleId>, IRoleRepository, ITransientDependency
{
    public RoleRepository(UserManagementContext context) : base(context)
    {
    }
}