using UM.Domain.Users.Repository;
using UM.Domain.Users;
using UM.Infrastructure.Utilities;

namespace UM.Infrastructure.Persistent.EFCore.Users;

public class UserRepository : BaseRepository<User, UserId>, IUserRepository
{
    public UserRepository(UserManagementContext context) : base(context)
    {
    }
}