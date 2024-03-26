using Common.Application.SecurityUtilities;
using MediatR;
using UM.Application.Users.AddToken;
using UM.Application.Users.ChangePassword;
using UM.Application.Users.Create;
using UM.Application.Users.Edit;
using UM.Application.Users.Register;
using UM.Application.Users.RemoveToken;
using UM.Application;
using UM.Query.Users.DTOs;
using UM.Query.Users.GetByFilter;
using UM.Query.Users.GetByPhoneNumber;
using UM.Query.Users.UserTokens.GetByRefreshToken;
using UM.Query.Users.GetById;
using UM.Domain.Users;
using UM.Query.Users.UserTokens.GetByJwtToken;
using UM.Application.Users.AddUserRole;

namespace UM.ServiceHost.Facade.Users
{
    public class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;
        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<OperationResult> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddToken(AddUserTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Status != OperationResultStatus.Success)
                return OperationResult.Error(result.Message);

            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<UserDto?> GetUserById(UserId userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));
        }

        public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
        {
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
        }

        public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
        {
            var hashJwtToken = Sha256Hasher.Hash(jwtToken);
            
            return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
        }

        public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
        {
            return await _mediator.Send(new GetUserByFilterQuery(filterParams));
        }

        public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
        }

        public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddUserRoles(AddUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
