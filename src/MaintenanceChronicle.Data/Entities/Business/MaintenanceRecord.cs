using System.ComponentModel.DataAnnotations.Schema;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Interfaces;
using NodaTime;

namespace MaintenanceChronicle.Data.Entities.Business;

[Table(nameof(MaintenanceRecord))]
public class MaintenanceRecord : ITrackable, ITenant
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;
    public Instant Date { get; set; }
    public RecordType Type { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; } = null!;
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
}
