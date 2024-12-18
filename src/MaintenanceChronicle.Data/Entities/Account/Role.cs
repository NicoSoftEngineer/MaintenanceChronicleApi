using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceChronicle.Data.Entities.Account;

[Table(nameof(Role))]
public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> Users { get; set; } = new HashSet<UserRole>();
}
