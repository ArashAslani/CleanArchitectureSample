using Common.Domain.Repository;

namespace UM.Domain.UserAgg.Repository;

public interface IUserRepository : IBaseRepository<User, UserId>
{
}
