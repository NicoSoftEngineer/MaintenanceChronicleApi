using MaintenanceChronicle.Data.Entities.Business;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordInListDto
{
    public Guid Id { get; set; }
    public required string LocationName { get; set; }
    public required string MachineName { get; set; }
    public required string MachineSerialNumber { get; set; }
    public required string Type { get; set; }
    public required string Date { get; set; }
    public required string Description { get; set; }
}

public static class MaintenanceRecordInListExtension
{
    public static MaintenanceRecordInListDto ToListDto(this MaintenanceRecord entity) => new MaintenanceRecordInListDto
    {
        Id = entity.Id,
        LocationName = entity.Machine.Location.Name,
        MachineName = entity.Machine.Model,
        MachineSerialNumber = entity.Machine.SerialNumber,
        Type = entity.Type.ToString(),
        Date = InstantPattern.CreateWithInvariantCulture("dd.MM.yyyy").Format(entity.Date),
        Description = entity.Description,
    };
}
