using MaintenanceChronicle.Utilities.Enum;

namespace MaintenanceChronicle.Data.Entities.Business;

public enum RecordType
{
    [MaintenanceRecordTypeEnumName("Installation")]
    Installation,
    [MaintenanceRecordTypeEnumName("Uninstallation")]
    UnInstallation,
    [MaintenanceRecordTypeEnumName("Maintenance")]
    Maintenance,
    [MaintenanceRecordTypeEnumName("Repair")]
    Repair,
}
