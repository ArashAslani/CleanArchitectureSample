using Microsoft.AspNetCore.Http;
using UM.Domain.UserAgg;
using UM.Domain.UserAgg.Enums;

namespace UM.Application.UserApplication.Edit;

public class EditUserCommand : IBaseCommand
{
    public EditUserCommand(UserId userId, IFormFile? avatar, string name, string family, string phoneNumber, string email, Gender gender)
    {
        UserId = userId;
        Avatar = avatar;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }
    public UserId UserId { get;  set; }
    public IFormFile? Avatar { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
}