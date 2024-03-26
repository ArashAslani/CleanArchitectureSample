using UM.Application.Users.AddToken;
using UM.Application.Users.ChangePassword;
using UM.Application.Users.Create;
using UM.Application.Users.Edit;
using UM.Application.Users.Register;
using UM.Application.Users.RemoveToken;
using UM.Application;
using UM.Query.Users.DTOs;
using UM.Application.Users.AddUserRole;
using UM.Domain.Users;

namespace UM.ServiceHost.Facade.Users
{
    public interface IUserFacade
    {

        //Commands
        Task<OperationResult> RegisterUser(RegisterUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> CreateUser(CreateUserCommand command);
        Task<OperationResult> AddToken(AddUserTokenCommand command);
        Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);
        Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
        Task<OperationResult> AddUserRoles(AddUserRoleCommand command);

        //Quries
        Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
        Task<UserDto?> GetUserById(UserId userId);
        Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken);
        Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);
        Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
    }
}
