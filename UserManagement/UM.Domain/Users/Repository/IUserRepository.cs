using Common.Domain.Repository;

namespace UM.Domain.Users.Repository;

public interface IUserRepository : IBaseRepository<User, UserId>
{
}
