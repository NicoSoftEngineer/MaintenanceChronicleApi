using ServiceTrack.Data.Entities.Account;

namespace ServiceTrack.Data.Interfaces;

public interface ITenant
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
