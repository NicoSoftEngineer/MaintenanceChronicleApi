using System.ComponentModel.DataAnnotations.Schema;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using NodaTime;

namespace MaintenanceChronicle.Data.Entities.Business;

[Table(nameof(Machine))]
public class Machine : ITrackable, ITenant
{
    public Guid Id { get; set; }
    public string Model { get; set; } = null!;
    public string Manufacture { get; set; } = null!;
    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public string Color { get; set; } = null!;
    public Instant InUseSince { get; set; }
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new HashSet<MaintenanceRecord>();
}
