namespace MaintenanceChronicle.Utilities.Options;

/// <summary>
/// Defines SMTP options.
/// </summary>
public class SmtpOptions
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
