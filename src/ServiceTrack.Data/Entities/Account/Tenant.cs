using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrack.Data.Entities.Account;

[Table(nameof(Tenant))]
public class Tenant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
