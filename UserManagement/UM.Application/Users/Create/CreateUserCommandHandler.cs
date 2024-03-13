using Common.Application.SecurityUtilities;
using UM.Domain.UserAgg;
using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg.Services;

namespace UM.Application.Users.Create;

internal class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _userDomainService;
    public CreateUserCommandHandler(IUserRepository repository, IUserDomainService userDomainService)
    {
        _repository = repository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = User.CreateNew(request.FirstName, request.LastName, request.PhoneNumber
            , request.Email, password, request.Gender, _userDomainService);

        await _repository.AddAsync(user);
        await _repository.SaveAsync();
        return OperationResult.Success();
    }
}
