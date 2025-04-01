using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Enum;
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
    public required string CustomerName { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceRecord"/> entity.
/// </summary>
public static class MaintenanceRecordInListExtension
{
    /// <summary>
    /// Convert <see cref="MaintenanceRecord"/> entity to <see cref="MaintenanceRecordInListDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="MaintenanceRecord"/> entity to map</param>
    /// <returns>Entity mapped to <see cref="MaintenanceRecordInListDto"/></returns>
    public static MaintenanceRecordInListDto ToListDto(this MaintenanceRecord entity) => new MaintenanceRecordInListDto
    {
        Id = entity.Id,
        LocationName = entity.Machine.Location.Name,
        MachineName = entity.Machine.Model,
        MachineSerialNumber = entity.Machine.SerialNumber,
        Type = entity.Type.GetTypeName(),
        Date = entity.Date.ToString(),
        Description = entity.Description,
        CustomerName = entity.Machine.Location.Customer.Name,
    };
}
