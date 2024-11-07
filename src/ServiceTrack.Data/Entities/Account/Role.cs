using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrack.Data.Entities.Account;

[Table(nameof(Role))]
public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> Users { get; set; } = new HashSet<UserRole>();
}
