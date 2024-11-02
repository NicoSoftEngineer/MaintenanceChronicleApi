namespace ServiceTrack.Utilities.Error;

public class BadRequestException(ErrorType errorType) : Exception
{
    public ErrorType ErrorType { get; } = errorType;
}
