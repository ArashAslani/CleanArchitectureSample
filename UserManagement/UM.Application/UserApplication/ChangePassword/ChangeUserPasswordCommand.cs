using Common.Application;
using UM.Domain.UserAgg;

namespace UM.Application.UserApplication.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public UserId UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
}