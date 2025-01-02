using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordDetailDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
    public required RecordType Type { get; set; }
}

public static class MaintenanceRecordDetailExtension
{
    public static MaintenanceRecordDetailDto ToDetailDto(this MaintenanceRecord entity) =>
        new MaintenanceRecordDetailDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Date = entity.Date,
            Type = entity.Type
        };
}
