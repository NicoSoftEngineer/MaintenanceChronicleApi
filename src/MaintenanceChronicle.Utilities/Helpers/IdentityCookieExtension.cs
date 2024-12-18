using System.Security.Claims;
using MaintenanceChronicle.Utilities.Constants;

namespace MaintenanceChronicle.Utilities.Helpers;

public static class IdentityCookieExtension
{
    public static string GetTenantId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(MaintenanceChronicleClaimTypes.TenantIdClaimType)!.Value;
    }

    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
}
