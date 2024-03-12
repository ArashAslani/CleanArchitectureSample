using Common.Domain.ValueObjects;

namespace UM.Application.UserApplication.Login;

internal class LoginUserCommand : IBaseCommand
{
    public LoginUserCommand(PhoneNumber phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Password { get; private set; }
}