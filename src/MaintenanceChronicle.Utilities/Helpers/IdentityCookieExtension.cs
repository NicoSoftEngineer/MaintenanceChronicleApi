using System.Security.Claims;
using MaintenanceChronicle.Utilities.Constants;

namespace MaintenanceChronicle.Utilities.Helpers;

public static class IdentityCookieExtension
{
    public static string? GetTenantId(this ClaimsPrincipal claimsPrincipal)
    {
        var tenantClaim = claimsPrincipal.FindFirst(MaintenanceChronicleClaimTypes.TenantIdClaimType);
        return tenantClaim?.Value;
    }

    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        return userClaim?.Value;
    }

    public static string? GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var userClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
        return userClaim?.Value;
    }
}
