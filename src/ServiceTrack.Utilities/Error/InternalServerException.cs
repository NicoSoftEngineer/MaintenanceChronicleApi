namespace ServiceTrack.Utilities.Error;

public class InternalServerException(List<string> errors) : Exception
{
    public List<string> Errors { get; } = errors;
}
