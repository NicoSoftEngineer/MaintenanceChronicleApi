using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;

public class NewMaintenanceRecordDto
{
    public Guid MachineId { get; set; }
    public required string Description { get; set; }
    public required string Date { get; set; }
    public required int RecordType { get; set; }
}
/// <summary>
/// Extension methods for NewMaintenanceRecordDto.
/// </summary>
public static class NewMaintenanceRecordExtension
{
    /// <summary>
    /// Converts <see cref="NewMaintenanceRecordDto"/> to <see cref="MaintenanceRecord"/>.
    /// </summary>
    /// <param name="dto"><see cref="NewMaintenanceRecordDto"/> to convert</param>
    /// <returns>Mapped entity to <see cref="MaintenanceRecord"/></returns>
    public static MaintenanceRecord ToEntity(this NewMaintenanceRecordDto dto) => new MaintenanceRecord
    {
        Type = (RecordType)dto.RecordType,
        MachineId = dto.MachineId,
        Date = InstantPattern.General.Parse(dto.Date.Split("T")[0]+"T12:00:00Z").Value,
        Description = dto.Description,
    };
}
