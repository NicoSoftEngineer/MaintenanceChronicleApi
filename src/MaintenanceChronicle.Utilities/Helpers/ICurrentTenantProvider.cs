namespace MaintenanceChronicle.Utilities.Helpers;

/// <summary>
/// Provides current tenant information from httpContext.
/// </summary>
public interface ICurrentTenantProvider
{
    public Guid TenantId { get; }
}
