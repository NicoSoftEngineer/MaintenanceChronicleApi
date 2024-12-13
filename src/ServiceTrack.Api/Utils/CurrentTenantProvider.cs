using ServiceTrack.Utilities.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using ServiceTrack.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace ServiceTrack.Api.Utils;

public class CurrentTenantProvider(IHttpContextAccessor httpContextAccessor)
    : ICurrentTenantProvider
{
    public Guid TenantId =>
        httpContextAccessor.HttpContext.User.FindFirstValue(ServiceTrackClaimTypes.TenantIdClaimType) != null
            ? new Guid(httpContextAccessor.HttpContext.User.FindFirstValue(ServiceTrackClaimTypes.TenantIdClaimType)!)
            : Guid.Empty;
}
