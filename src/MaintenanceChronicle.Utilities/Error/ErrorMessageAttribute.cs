namespace MaintenanceChronicle.Utilities.Error;
/// <summary>
/// Attribute for enum values to define error messages.
/// </summary>
/// <param name="message">Error message</param>
public class ErrorMessageAttribute(string message) : Attribute
{
    public string Message { get; } = message;
}
