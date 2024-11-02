using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrack.Data.Entities;

[Table(nameof(UserRole))]
public class UserRole : IdentityUserRole<Guid>
{
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
}
