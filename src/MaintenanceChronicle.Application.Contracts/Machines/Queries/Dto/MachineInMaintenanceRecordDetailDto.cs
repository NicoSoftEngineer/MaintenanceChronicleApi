using MaintenanceChronicle.Data.Entities.Business;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineInMaintenanceRecordDetailDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string Manufacture { get; set; }
    public required string SerialNumber { get; set; }
    public required string LocationName { get; set; }
    public required string Color { get; set; }
    public required string InUseSince { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceRecord"/> entity.
/// </summary>
public static class MachineInMaintenanceRecordDetailExtension
{
    /// <summary>
    /// Converts <see cref="MaintenanceRecord"/> to <see cref="MachineInMaintenanceRecordDetailDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="MaintenanceRecord"/> to convert</param>
    /// <returns>Converted entity to  <see cref="MachineInMaintenanceRecordDetailDto"/></returns>
    public static MachineInMaintenanceRecordDetailDto ToMachineDto(this MaintenanceRecord entity) =>
        new MachineInMaintenanceRecordDetailDto
        {
            Id = entity.MachineId,
            Model = entity.Machine.Model,
            Manufacture = entity.Machine.Manufacture,
            Color = entity.Machine.Color,
            InUseSince =  InstantPattern.CreateWithInvariantCulture("dd.MM.yyyy").Format(entity.Machine.InUseSince),
            SerialNumber = entity.Machine.SerialNumber,
            LocationName = entity.Machine.Location.Name,
        };
}
