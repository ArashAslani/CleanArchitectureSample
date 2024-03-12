using Common.Application.SecurityUtilities;
using UM.Domain.UserAgg;
using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg.Services;

namespace UM.Application.UserApplication.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _userDomainService;
    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService userDomainService)
    {
        _repository = repository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = User.CreateNew(request.Name, request.Family, request.PhoneNumber
            , request.Email, password, request.Gender, _userDomainService);

        await _repository.AddAsync(user);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}
