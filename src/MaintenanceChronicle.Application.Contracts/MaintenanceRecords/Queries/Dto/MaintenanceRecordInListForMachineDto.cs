using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordInListForMachineDto
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Date { get; set; }
    public required string Description { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceRecord"/> entity.
/// </summary>
public static class MaintenanceRecordInListForMachineExtension
{
    /// <summary>
    /// Converts <see cref="MaintenanceRecord"/> entity to <see cref="MaintenanceRecordInListForMachineDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="MaintenanceRecord"/> entity to be mapped</param>
    /// <returns>Mapped entity to <see cref="MaintenanceRecordInListForMachineDto"/></returns>
    public static MaintenanceRecordInListForMachineDto ToListForMachineDto(this MaintenanceRecord entity) => new MaintenanceRecordInListForMachineDto
    {
        Id = entity.Id,
        Type = entity.Type.GetTypeName(),
        Date = InstantPattern.CreateWithInvariantCulture("dd.MM.yyyy").Format(entity.Date),
        Description = entity.Description,
    };
}
