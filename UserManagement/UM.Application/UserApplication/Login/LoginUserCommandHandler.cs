using Common.Application.SecurityUtilities;
using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg.Services;
using UM.Domain.UserAgg;

namespace UM.Application.UserApplication.Login;

internal class LoginUserCommandHandler : IBaseCommandHandler<LoginUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public LoginUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.LoginUser(request.PhoneNumber.Value, Sha256Hasher.Hash(request.Password), _domainService);

        await _repository.AddAsync(user);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}