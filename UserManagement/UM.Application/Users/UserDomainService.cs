using UM.Domain.UserAgg.Repository;
using UM.Domain.UserAgg.Services;

namespace UM.Application.Users;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _repository;

    public UserDomainService(IUserRepository repository)
    {
        _repository = repository;
    }

    public bool IsEmailExist(string email)
    {
        return _repository.Exists(r => r.Email == email);
    }

    public bool PhoneNumberIsExist(string phoneNumber)
    {
        return _repository.Exists(r => r.PhoneNumber == phoneNumber);

    }
}
