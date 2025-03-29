using System.Security.Claims;
using MaintenanceChronicle.Utilities.Constants;

namespace MaintenanceChronicle.Utilities.Helpers;

/// <summary>
/// Provides extension methods for ClaimsPrincipal.
/// </summary>
public static class IdentityCookieExtension
{
    /// <summary>
    /// Gets the tenant id from the claims principal.
    /// </summary>
    /// <param name="claimsPrincipal">User claims principal</param>
    /// <returns>Tenant id or null</returns>
    public static string? GetTenantId(this ClaimsPrincipal claimsPrincipal)
    {
        var tenantClaim = claimsPrincipal.FindFirst(MaintenanceChronicleClaimTypes.TenantIdClaimType);
        return tenantClaim?.Value;
    }

    /// <summary>
    /// Gets the user id from the claims principal.
    /// </summary>
    /// <param name="claimsPrincipal">User claims principal</param>
    /// <returns>User id or null</returns>
    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        return userClaim?.Value;
    }

    /// <summary>
    /// Gets the user email from the claims principal.
    /// </summary>
    /// <param name="claimsPrincipal">User claims principal</param>
    /// <returns>User email or null</returns>
    public static string? GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var userClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
        return userClaim?.Value;
    }

    /// <summary>
    /// Gets the user roles from the claims principal.
    /// </summary>
    /// <param name="claimsPrincipal">User claims principal</param>
    /// <returns>Users role names in array</returns>
    public static string[] GetUserRoles(this ClaimsPrincipal claimsPrincipal)
    {
        var userRoles = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToArray();
        return userRoles;
    }
}
