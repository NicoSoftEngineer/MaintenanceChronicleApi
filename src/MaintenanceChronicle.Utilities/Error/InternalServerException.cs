namespace MaintenanceChronicle.Utilities.Error;
/// <summary>
/// Internal server exception to be thrown if something goes wrong.
/// </summary>
public class InternalServerException : Exception
{
    public List<string> Errors { get; } = new ();
    /// <summary>
    /// Constructor for InternalServerException.
    /// </summary>
    /// <param name="error">One error that happened</param>
    public InternalServerException(string error)
    {
        Errors.Add(error);
    }

    /// <summary>
    /// Constructor for InternalServerException.
    /// </summary>
    /// <param name="errors">Multiple errors that happened</param>
    public InternalServerException(List<string> errors)
    {
        Errors = errors;
    }
}
