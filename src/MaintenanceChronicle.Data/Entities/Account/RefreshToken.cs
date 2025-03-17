using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaintenanceChronicle.Data.Entities.Account;

[Table(nameof(RefreshToken))]
public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Token { get; set; } = null!;
    public Instant CreatedAt { get; set; }
    public Instant ExpiresAt { get; set; }
    public Instant? RevokedAt { get; set; }
    public string? RequestInfo { get; set; }
}
