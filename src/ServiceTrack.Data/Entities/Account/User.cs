using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NodaTime;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Data.Entities.Account;

[Table(nameof(User))]
public class User : IdentityUser<Guid>, ITenant, ITrackable
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
