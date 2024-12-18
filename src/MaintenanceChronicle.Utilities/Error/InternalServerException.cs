namespace MaintenanceChronicle.Utilities.Error;

public class InternalServerException : Exception
{
    public List<string> Errors { get; } = new ();

    public InternalServerException(string error)
    {
        Errors.Add(error);
    }

    public InternalServerException(List<string> errors)
    {
        Errors = errors;
    }
}
