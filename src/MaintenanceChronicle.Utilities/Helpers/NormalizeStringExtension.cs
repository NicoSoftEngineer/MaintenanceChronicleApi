namespace MaintenanceChronicle.Utilities.Helpers;

public static class NormalizeStringExtension
{
    public static string NormalizeToUpper(this string value)
    {
        return new string(value.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray()!).ToUpperInvariant();
    }
}
