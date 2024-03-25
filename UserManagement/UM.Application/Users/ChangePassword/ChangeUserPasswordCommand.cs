using UM.Domain.Users;

namespace UM.Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public UserId UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
}