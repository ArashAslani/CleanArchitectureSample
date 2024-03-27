using UM.Domain.Users;

namespace UM.ServiceHost.Facade;

internal class CacheKeys
{
    public static string User(UserId id) => $"user-{id}";
    public static string UserToken(string hashToken) => $"tok-{hashToken}";
}