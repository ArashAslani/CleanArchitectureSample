using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using UM.Domain.UserAgg.Enums;
using UM.Domain.UserAgg.Services;

namespace UM.Domain.UserAgg
{
    public class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string AvatarName { get; set; }
        public bool IsActive { get; set; }

        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; }
        public List<UserToken> Tokens { get; }

        private User()
        {

        }

        private User(UserId id, string firstName, string lastName, string phoneNumber, string email,
            string password, Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);

            Id = id;

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
            AvatarName = "avatar.png";
            IsActive = true;
            Roles = [];
            Tokens = [];
        }


        public static User CreateNew(string firstName, string lastName, string phoneNumber, string email,
            string password, Gender gender, IUserDomainService userDomainService)
        {
            var userId = new UserId(Guid.NewGuid());
            return new User(userId, firstName, lastName, phoneNumber, email, password, gender, userDomainService);
        }


        public void Edit(string firstName, string lastName, string phoneNumber, string email,
            Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public void ChangePassword(string newPassword)
        {
            NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

            Password = newPassword;
        }
        public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
        {
            var userId = new UserId(Guid.NewGuid());
            return new User(userId, "", "", phoneNumber, null, password, Gender.None, userDomainService);
        }

        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";

            AvatarName = imageName;
        }
       
       

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }

        public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
        {
            var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
            if (activeTokenCount == 3)
                throw new InvalidDomainDataException("It is not possible to use 4 devices at the same time.");

            var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device)
            {
                UserId = Id
            };
            Tokens.Add(token);
        }

        public string RemoveToken(long tokenId)
        {
            var token = Tokens.FirstOrDefault(f => f.Id == tokenId) ?? throw new InvalidDomainDataException("invalid TokenId");
            Tokens.Remove(token);
            return token.HashJwtToken;
        }
        public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("Phone number is invalid.");

            if (!string.IsNullOrWhiteSpace(email))
                if (email.IsValidEmail() == false)
                    throw new InvalidDomainDataException("Email is invalid.");

            if (phoneNumber != PhoneNumber)
                if (userDomainService.PhoneNumberIsExist(phoneNumber))
                    throw new InvalidDomainDataException("The phone number is repeated.");

            if (email != Email)
                if (userDomainService.IsEmailExist(email))
                    throw new InvalidDomainDataException("Email is repetitive.");
        }
    }
}
