using Common.Query;
using UM.Domain.Users;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQuery : IQuery<UserDto?>
{
    public GetUserByPhoneNumberQuery(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; private set; }
}