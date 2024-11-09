using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Data.Entities.Account;

[Table(nameof(Tenant))]
public class Tenant : ITrackable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
