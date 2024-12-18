namespace MaintenanceChronicle.Utilities.Helpers;

public interface ICurrentTenantProvider
{
    public Guid TenantId { get; }
}
