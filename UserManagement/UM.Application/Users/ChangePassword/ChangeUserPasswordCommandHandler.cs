using Common.Application.SecurityUtilities;
using UM.Domain.Users.Repository;

namespace UM.Application.Users.ChangePassword;

public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound("کاربر یافت نشد");

        var currentPasswordHash = Sha256Hasher.Hash(request.CurrentPassword);
        if (user.Password != currentPasswordHash)
        {
            return OperationResult.Error("کلمه عبور فعلی نامعتبر است");
        }

        var newPasswordHash = Sha256Hasher.Hash(request.Password);
        user.ChangePassword(newPasswordHash);
        await _userRepository.SaveAsync();

        return OperationResult.Success();
    }
}