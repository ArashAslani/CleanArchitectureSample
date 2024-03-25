using Common.Query;
using UM.Domain.Users;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetById;

public class GetUserByIdQuery : IQuery<UserDto?>
{
    public GetUserByIdQuery(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; private set; }
}