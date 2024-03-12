using UM.Domain.UserAgg;

namespace UM.Application.UserApplication.RemoveToken;

public record RemoveUserTokenCommand(UserId UserId,long TokenId) : IBaseCommand<string>;