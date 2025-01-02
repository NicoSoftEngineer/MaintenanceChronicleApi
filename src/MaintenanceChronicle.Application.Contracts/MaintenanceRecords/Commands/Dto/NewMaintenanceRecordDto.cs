using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;

public class NewMaintenanceRecordDto
{
    public Guid MachineId { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
    public required RecordType RecordType { get; set; }
}

public static class NewMaintenanceRecordExtension
{
    public static MaintenanceRecord ToEntity(this NewMaintenanceRecordDto dto) => new MaintenanceRecord
    {
        Type = dto.RecordType,
        MachineId = dto.MachineId,
        Date = dto.Date,
        Description = dto.Description,
    };
}
