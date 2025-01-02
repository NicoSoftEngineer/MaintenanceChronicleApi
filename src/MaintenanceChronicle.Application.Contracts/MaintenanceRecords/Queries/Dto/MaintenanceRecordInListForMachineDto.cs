using MaintenanceChronicle.Data.Entities.Business;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordInListForMachineDto
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Date { get; set; }
    public required string Description { get; set; }
}

public static class MaintenanceRecordInListForMachineExtension
{
    public static MaintenanceRecordInListForMachineDto ToListForMachineDto(this MaintenanceRecord entity) => new MaintenanceRecordInListForMachineDto
    {
        Id = entity.Id,
        Type = entity.Type.ToString(),
        Date = InstantPattern.CreateWithInvariantCulture("dd.MM.yyyy").Format(entity.Date),
        Description = entity.Description,
    };
}
