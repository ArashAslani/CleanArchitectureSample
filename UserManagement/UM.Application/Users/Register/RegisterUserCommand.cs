using Common.Domain.ValueObjects;

namespace UM.Application.Users.Register;

public class RegisterUserCommand(PhoneNumber phoneNumber, string password) : IBaseCommand
{
    public PhoneNumber PhoneNumber { get; private set; } = phoneNumber;
    public string Password { get; private set; } = password;
}