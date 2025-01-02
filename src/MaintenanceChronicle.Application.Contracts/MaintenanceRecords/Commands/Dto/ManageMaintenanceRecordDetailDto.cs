using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;

public class ManageMaintenanceRecordDetailDto
{
    public Guid Id { get; set; }
    public Guid MachineId { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
    public required RecordType Type { get; set; }
}

public static class MaintenanceRecordDetailExtension
{
    public static ManageMaintenanceRecordDetailDto ToManageDto(this MaintenanceRecord entity) => new ManageMaintenanceRecordDetailDto()
    {
        MachineId = entity.MachineId,
        Description = entity.Description,
        Date = entity.Date,
        Type = entity.Type,
    };

    public static void MapToEntity(this ManageMaintenanceRecordDetailDto dto, MaintenanceRecord target)
    {
        target.MachineId = dto.MachineId;
        target.Description = dto.Description;
        target.Date = dto.Date;
        target.Type = dto.Type;
    }
}
