using System.Globalization;
using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;

public class ManageMaintenanceRecordDetailDto
{
    public Guid Id { get; set; }
    public Guid MachineId { get; set; }
    public required string Description { get; set; }
    public required string Date { get; set; }
    public required RecordType Type { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceRecord"/> entity.
/// </summary>
public static class MaintenanceRecordDetailExtension
{
    /// <summary>
    /// Converts <see cref="MaintenanceRecord"/> to <see cref="ManageMaintenanceRecordDetailDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="MaintenanceRecord"/> to convert</param>
    /// <returns>Converted entity to <see cref="ManageMaintenanceRecordDetailDto"/></returns>
    public static ManageMaintenanceRecordDetailDto ToManageDto(this MaintenanceRecord entity) => new ManageMaintenanceRecordDetailDto()
    {
        MachineId = entity.MachineId,
        Description = entity.Description,
        Date = entity.Date.ToString(),
        Type = entity.Type,
    };

    /// <summary>
    /// Maps <see cref="ManageMaintenanceRecordDetailDto"/> to <see cref="MaintenanceRecord"/>.
    /// </summary>
    /// <param name="dto">Source <see cref="ManageMaintenanceRecordDetailDto"/></param>
    /// <param name="target">Destination <see cref="MaintenanceRecord"/></param>
    public static void MapToEntity(this ManageMaintenanceRecordDetailDto dto, MaintenanceRecord target)
    {
        target.MachineId = dto.MachineId;
        target.Description = dto.Description;
        target.Date = InstantPattern.General.Parse(dto.Date.Split("T")[0] + "T12:00:00Z").Value;
        target.Type = dto.Type;
    }
}
