using System.Security.Cryptography;
using System.Text;

namespace MaintenanceChronicle.Utilities.Helpers;

/// <summary>
/// Extension methods for string class
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Normalizes string to upper case
    /// </summary>
    /// <param name="value">String to be normalized</param>
    /// <returns>Normalized string</returns>
    public static string NormalizeToUpper(this string value)
    {
        return new string(value.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray()!).ToUpperInvariant();
    }

    /// <summary>
    /// Hashes input string using SHA256
    /// </summary>
    /// <param name="value">string to be hashed</param>
    /// <returns>Hashed string</returns>
    public static string Hash(this string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Converts string to its escaped representation
    /// </summary>
    /// <param name="value">String to be converted</param>
    /// <returns>Converted string</returns>
    public static string UriEscape(this string value) => Uri.EscapeDataString(value);
}
