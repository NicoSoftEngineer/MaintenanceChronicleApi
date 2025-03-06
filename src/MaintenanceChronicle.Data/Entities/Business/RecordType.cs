using MaintenanceChronicle.Utilities.Enum;

namespace MaintenanceChronicle.Data.Entities.Business;

public enum RecordType
{
    [MaintenanceRecordTypeEnumName("Instalace")]
    Installation,
    [MaintenanceRecordTypeEnumName("Odinstalace")]
    UnInstallation,
    [MaintenanceRecordTypeEnumName("Údržba")]
    Maintenance,
    [MaintenanceRecordTypeEnumName("Oprava")]
    Repair,
}
