namespace MaintenanceChronicle.Utilities.Error;

/// <summary>
/// Exception to be thrown if the request is unauthorized.
/// </summary>
/// <param name="errorType">Reason why the error is thrown</param>
public class UnauthorizedRequestException(ErrorType errorType) : Exception
{
    public ErrorType ErrorType { get; } = errorType;
}
