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

public static class NewMaintenanceRecordExtension
{
    public static MaintenanceRecord ToEntity(this NewMaintenanceRecordDto dto) => new MaintenanceRecord
    {
        Type = (RecordType)dto.RecordType,
        MachineId = dto.MachineId,
        Date = InstantPattern.General.Parse(dto.Date.Split("T")[0]+"T12:00:00Z").Value,
        Description = dto.Description,
    };
}
