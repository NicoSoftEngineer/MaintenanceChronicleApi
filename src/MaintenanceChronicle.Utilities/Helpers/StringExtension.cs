using System.Security.Cryptography;
using System.Text;

namespace MaintenanceChronicle.Utilities.Helpers;

public static class StringExtension
{
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
}
