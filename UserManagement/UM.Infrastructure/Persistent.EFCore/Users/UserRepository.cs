using UM.Domain.Users.Repository;
using UM.Domain.Users;
using UM.Infrastructure.Utilities;
using Common.DotNetCore.Utilities.DependencyInjection;

namespace UM.Infrastructure.Persistent.EFCore.Users;

public class UserRepository : BaseRepository<User, UserId>, IUserRepository, ITransientDependency
{
    public UserRepository(UserManagementContext context) : base(context)
    {
    }
}