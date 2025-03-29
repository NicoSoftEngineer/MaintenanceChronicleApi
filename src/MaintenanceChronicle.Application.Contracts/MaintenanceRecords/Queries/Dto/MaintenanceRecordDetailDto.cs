using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordDetailDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
    public required string Type { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceRecordDetailDto"/>.
/// </summary>
public static class MaintenanceRecordDetailExtension
{
    /// <summary>
    /// Converts <see cref="MaintenanceRecord"/> to <see cref="MaintenanceRecordDetailDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="MaintenanceRecord"/> entity to map</param>
    /// <returns>Mapped entity to <see cref="MaintenanceRecordDetailDto"/></returns>
    public static MaintenanceRecordDetailDto ToDetailDto(this MaintenanceRecord entity) =>
        new MaintenanceRecordDetailDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Date = entity.Date,
            Type = entity.Type.GetTypeName()
        };
}
