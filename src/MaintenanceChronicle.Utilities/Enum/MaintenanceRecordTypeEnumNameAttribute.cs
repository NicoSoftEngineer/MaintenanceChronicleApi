namespace MaintenanceChronicle.Utilities.Enum;
/// <summary>
/// Attribute for enum values to define names.
/// </summary>
/// <param name="name">The name for enum</param>
public class MaintenanceRecordTypeEnumNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
