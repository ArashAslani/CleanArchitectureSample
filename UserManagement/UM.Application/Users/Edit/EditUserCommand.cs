using Microsoft.AspNetCore.Http;
using UM.Domain.UserAgg;
using UM.Domain.UserAgg.Enums;

namespace UM.Application.Users.Edit;

public class EditUserCommand(UserId userId, IFormFile? avatar, string name, string family, string phoneNumber, string email, Gender gender) : IBaseCommand
{
    public UserId UserId { get; set; } = userId;
    public IFormFile? Avatar { get; private set; } = avatar;
    public string Name { get; private set; } = name;
    public string Family { get; private set; } = family;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string Email { get; private set; } = email;
    public Gender Gender { get; private set; } = gender;
}