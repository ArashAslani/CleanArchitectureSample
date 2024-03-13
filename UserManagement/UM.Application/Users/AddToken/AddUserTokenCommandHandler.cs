using UM.Domain.UserAgg.Repository;

namespace UM.Application.Users.AddToken;

internal class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _repository;

    public AddUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsTrackingAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound();


        user.AddToken(request.HashJwtToken, request.HashRefreshToken, request.TokenExpireDate, request.RefreshTokenExpireDate, request.Device);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}