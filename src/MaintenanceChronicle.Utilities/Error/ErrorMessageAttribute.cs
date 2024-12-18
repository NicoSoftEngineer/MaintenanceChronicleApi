namespace MaintenanceChronicle.Utilities.Error;

public class ErrorMessageAttribute(string message) : Attribute
{
    public string Message { get; } = message;
}
