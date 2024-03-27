using System.Security.Claims;

namespace Common.DotNetCore.Utilities;

public static class ClaimExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return principal == null
            ? throw new ArgumentNullException(nameof(principal))
            : Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    public static string GetPhoneNumber(this ClaimsPrincipal principal)
    {
        return principal == null ? throw new ArgumentNullException(nameof(principal)) : (principal.FindFirst(ClaimTypes.MobilePhone)?.Value);
    }
}