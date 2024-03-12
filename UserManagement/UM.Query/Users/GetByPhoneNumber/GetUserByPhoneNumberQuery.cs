using Common.Query;
using UM.Query.Users.DTOs;

namespace UM.Query.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<UserDto?>;