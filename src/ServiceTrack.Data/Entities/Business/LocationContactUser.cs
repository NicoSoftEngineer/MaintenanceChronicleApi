using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Data.Entities.Business;
[Table(nameof(LocationContactUser))]
public class LocationContactUser : ITrackable, ITenant
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
