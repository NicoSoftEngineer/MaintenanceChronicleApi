using ServiceTrack.Utilities.Constants;
using System.Security.Claims;

namespace ServiceTrack.Utilities.Helpers;

public static class IdentityCookieExtension
{
    public static string GetTenantId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ServiceTrackClaimTypes.TenantIdClaimType)!.Value;
    }

    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
}
