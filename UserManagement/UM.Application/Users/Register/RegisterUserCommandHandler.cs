using Common.Application.SecurityUtilities;
using UM.Domain.Users.Repository;
using UM.Domain.Users.Services;
using UM.Domain.Users;

namespace UM.Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.RegisterUser(request.PhoneNumber.Value, Sha256Hasher.Hash(request.Password), _domainService);

        await _repository.AddAsync(user);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}