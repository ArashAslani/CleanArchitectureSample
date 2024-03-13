using UM.Domain.UserAgg.Enums;

namespace UM.Application.Users.Create;

public class CreateUserCommand : IBaseCommand
{
    public CreateUserCommand(string firstName, string lastName, string phoneNumber, string email, string password, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Gender Gender { get; private set; }
}