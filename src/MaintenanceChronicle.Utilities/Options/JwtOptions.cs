namespace MaintenanceChronicle.Utilities.Options;

/// <summary>
/// Defines JWT options.
/// </summary>
public class JwtOptions
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int AccessTokenExpirationInMinutes { get; set; }
    public required int RefreshTokenExpirationInDays { get; set; }
}
