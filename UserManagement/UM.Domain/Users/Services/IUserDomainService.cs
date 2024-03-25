namespace UM.Domain.Users.Services;

public interface IUserDomainService
{
    bool IsEmailExist(string email);

    bool PhoneNumberIsExist(string phoneNumber);
}
