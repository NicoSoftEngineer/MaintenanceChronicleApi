using System.ComponentModel.DataAnnotations.Schema;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using NodaTime;

namespace MaintenanceChronicle.Data.Entities.Business;

[Table(nameof(Customer))]
public class Customer : ITrackable, ITenant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Location> Locations { get; set; } = new HashSet<Location>();
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
}
