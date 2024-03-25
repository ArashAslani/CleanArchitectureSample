using UM.Domain.Users.Enums;

namespace UM.Application.Users.Create;

public class CreateUserCommand(string firstName, string lastName, string phoneNumber, string email, string password, Gender gender) : IBaseCommand
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public Gender Gender { get; private set; } = gender;
}