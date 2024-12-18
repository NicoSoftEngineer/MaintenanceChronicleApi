using MaintenanceChronicle.Utilities.Error;

namespace MaintenanceChronicle.Utilities.Enum;

public static class EnumExtensions
{
    public static string GetErrorMessage(this System.Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field!, typeof(ErrorMessageAttribute)) as ErrorMessageAttribute;
        return attribute!.Message;
    }
}
