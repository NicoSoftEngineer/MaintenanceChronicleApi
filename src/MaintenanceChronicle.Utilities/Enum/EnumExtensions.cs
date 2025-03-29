using MaintenanceChronicle.Utilities.Error;

namespace MaintenanceChronicle.Utilities.Enum;
/// <summary>
/// Extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Get the error message for the enum value.
    /// </summary>
    /// <param name="value">The enum to get the error from</param>
    /// <returns>The error message for enum</returns>
    public static string GetErrorMessage(this System.Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field!, typeof(ErrorMessageAttribute)) as ErrorMessageAttribute;
        return attribute!.Message;
    }

    /// <summary>
    /// Get the name for the enum value.
    /// </summary>
    /// <param name="value">The enum to get the name from</param>
    /// <returns>Name of enum</returns>
    public static string GetTypeName(this System.Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field!, typeof(MaintenanceRecordTypeEnumNameAttribute)) as MaintenanceRecordTypeEnumNameAttribute;
        return attribute!.Name;
    }
}
