using MaintenanceChronicle.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace MaintenanceChronicle.Utilities.Cookies;

public class DynamicCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor) : CookieAuthenticationEvents
{
    public override Task SigningIn(CookieSigningInContext context)
    {
        var httpContext = httpContextAccessor.HttpContext;

        // Example: Generate dynamic cookie name based on user role or tenant
        var user = context.Principal;
        var tenantId = user.FindFirst(MaintenanceChronicleClaimTypes.TenantIdClaimType)?.Value ?? "DefaultTenant";
        var uniqueCookieName = $"AuthCookie_{tenantId}";


        // Set a dynamic cookie name
        context.Options.Cookie.Name = uniqueCookieName;
        return Task.CompletedTask;
    }
}
