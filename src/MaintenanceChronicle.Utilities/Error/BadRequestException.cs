namespace MaintenanceChronicle.Utilities.Error;

/// <summary>
/// Exception to be thrown if the request is bad.
/// </summary>
/// <param name="errorType">Error type defined by enum</param>
/// <param name="propName">Which prop is bad</param>
public class BadRequestException(ErrorType errorType, string? propName = null) : Exception
{
    public string? PropertyName { get; set; } = propName;
    public ErrorType ErrorType { get; } = errorType;
}
