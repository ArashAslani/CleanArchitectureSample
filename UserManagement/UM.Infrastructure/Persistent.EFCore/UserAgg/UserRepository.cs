using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg;
using UM.Infrastructure.Utilities;

namespace UM.Infrastructure.Persistent.EFCore.UserAgg;

public class UserRepository : BaseRepository<User, UserId>, IUserRepository
{
    public UserRepository(UserManagmentContext context) : base(context)
    {
    }
}