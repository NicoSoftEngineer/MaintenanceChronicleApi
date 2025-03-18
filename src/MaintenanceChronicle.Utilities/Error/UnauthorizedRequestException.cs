namespace MaintenanceChronicle.Utilities.Error;

public class UnauthorizedRequestException(ErrorType errorType) : Exception
{
    public ErrorType ErrorType { get; } = errorType;
}
