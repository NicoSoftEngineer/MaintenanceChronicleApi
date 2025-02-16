namespace MaintenanceChronicle.Utilities.Error;

public class BadRequestException(ErrorType errorType, string? propName = null) : Exception
{
    public string? PropertyName { get; set; } = propName;
    public ErrorType ErrorType { get; } = errorType;
}
