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
using Microsoft.Extensions.Caching.Distributed;
using Common.DotNetCore.Utilities;
using Common.DotNetCore.Utilities.DependencyInjection;

namespace UM.ServiceHost.Facade.Users
{
    public class UserFacade : IUserFacade, IScopedDependency
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;
        public UserFacade(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
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

            await _cache.RemoveAsync(CacheKeys.UserToken(result.Data));

            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            await _cache.RemoveAsync(CacheKeys.User(command.UserId));
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Status == OperationResultStatus.Success)
                await _cache.RemoveAsync(CacheKeys.User(command.UserId));
            return result;
        }

        public async Task<UserDto?> GetUserById(UserId userId)
        {
            return await _cache.GetOrSet(CacheKeys.User(userId), () =>
            {
                return _mediator.Send(new GetUserByIdQuery(userId));
            }, 
            new CacheOptions 
            { 
                AbsoluteExpirationCacheFromMinutes = 5,
                ExpireSlidingCacheFromMinutes = 10
            });
        }

        public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
        {
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
        }

        public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
        {
            var hashJwtToken = Sha256Hasher.Hash(jwtToken);

            return await _cache.GetOrSet(CacheKeys.UserToken(hashJwtToken), () =>
            {
                return _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
            });
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
