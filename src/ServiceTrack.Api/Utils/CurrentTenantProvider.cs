using ServiceTrack.Utilities.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using ServiceTrack.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace ServiceTrack.Api.Utils;

public class CurrentTenantProvider(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
    : ICurrentTenantProvider
{
    private readonly IDataProtector dataProtector = dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", "Identity.Application", "v2");

    public Guid TenantId => GetTenantId();

    public Guid GetTenantId()
    {
        var cookie = httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Identity.Application"];
        if (cookie == null)
        {
            return Guid.Empty;
        }

        var ticketDataFormat = new TicketDataFormat(dataProtector);
        var ticket = ticketDataFormat.Unprotect(cookie);
        var user = ticket?.Principal;

        if (user == null)
        {
            return Guid.Empty;
        }

        var tenantId = new Guid(user.FindFirstValue(ServiceTrackClaimTypes.TenantIdClaimType)!);

        return tenantId;
    }
}
