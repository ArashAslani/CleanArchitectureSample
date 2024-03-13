using UM.Domain.UserAgg;

namespace UM.Application.Users.RemoveToken;

public record RemoveUserTokenCommand(UserId UserId,long TokenId) : IBaseCommand<string>;