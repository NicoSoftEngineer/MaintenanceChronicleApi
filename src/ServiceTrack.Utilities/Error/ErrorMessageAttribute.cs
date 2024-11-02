namespace ServiceTrack.Utilities.Error;

public class ErrorMessageAttribute(string message) : Attribute
{
    public string Message { get; } = message;
}
