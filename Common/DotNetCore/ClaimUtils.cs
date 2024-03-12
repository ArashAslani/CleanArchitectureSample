using System.Security.Claims;

namespace Common.DotNetCore;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        return principal == null
            ? throw new ArgumentNullException(nameof(principal))
            : Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    public static string GetPhoneNumber(this ClaimsPrincipal principal)
    {
        return principal == null ? throw new ArgumentNullException(nameof(principal)) : (principal.FindFirst(ClaimTypes.MobilePhone)?.Value);
    }
}