namespace MaintenanceChronicle.Utilities.Enum;

public class MaintenanceRecordTypeEnumNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
