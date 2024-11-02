using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceTrack.Data.Entities;

[Table(nameof(User))]
public class Tenant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
