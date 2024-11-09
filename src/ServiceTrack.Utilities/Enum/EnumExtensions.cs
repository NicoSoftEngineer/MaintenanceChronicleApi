using ServiceTrack.Utilities.Error;

namespace ServiceTrack.Utilities.Enum;

public static class EnumExtensions
{
    public static string GetErrorMessage(this System.Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field!, typeof(ErrorMessageAttribute)) as ErrorMessageAttribute;
        return attribute!.Message;
    }
}
