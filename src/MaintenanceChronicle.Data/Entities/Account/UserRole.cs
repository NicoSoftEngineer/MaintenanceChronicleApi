using System.ComponentModel.DataAnnotations.Schema;
using MaintenanceChronicle.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace MaintenanceChronicle.Data.Entities.Account;

[Table(nameof(UserRole))]
public class UserRole : IdentityUserRole<Guid>, ITenant, ITrackable
{
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
