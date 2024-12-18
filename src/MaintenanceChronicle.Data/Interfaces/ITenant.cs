using MaintenanceChronicle.Data.Entities.Account;

namespace MaintenanceChronicle.Data.Interfaces;

public interface ITenant
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
