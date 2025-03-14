namespace MaintenanceChronicle.Utilities.Options;

public class EnvironmentOptions
{
    public required string FrontendHostUrl { get; set; }
    public required string FrontendConfirmUrl { get; set; }
    public required string FrontendPasswordResetUrl { get; set; }
    public required string FrontendPasswordCreateUrl { get; set; }
    public required string SenderEmail { get; set; }
    public required string SenderName { get; set; }
}
