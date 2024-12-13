using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using ServiceTrack.Data.Entities.Account;
using ServiceTrack.Data.Interfaces;

namespace ServiceTrack.Data.Entities.Business;

[Table(nameof(Location))]
public class Location : ITrackable, ITenant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Instant CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public Instant ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public Instant? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
}
