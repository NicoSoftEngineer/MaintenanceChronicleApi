using System.Security.Claims;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;

namespace MaintenanceChronicle.Api.Utils;

public class CurrentTenantProvider(IHttpContextAccessor httpContextAccessor)
    : ICurrentTenantProvider
{
    public Guid TenantId =>
        httpContextAccessor.HttpContext!.User.FindFirstValue(MaintenanceChronicleClaimTypes.TenantIdClaimType) != null
            ? new Guid(httpContextAccessor.HttpContext.User.FindFirstValue(MaintenanceChronicleClaimTypes.TenantIdClaimType)!)
            : Guid.Empty;
}
