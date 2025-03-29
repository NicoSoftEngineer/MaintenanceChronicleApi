using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineInListForLocationDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string SerialNumber { get; set; }
}
/// <summary>
/// Extension methods for <see cref="Machine"/> entity.
/// </summary>
public static class MachineInListForLocationExtension
{
    /// <summary>
    /// Converts <see cref="Machine"/> to <see cref="MachineInListForLocationDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="Machine"/> to convert</param>
    /// <returns>Converted entity to <see cref="MachineInListForLocationDto"/></returns>
    public static MachineInListForLocationDto ToMachineInListForLocationDto(this Machine entity) => new MachineInListForLocationDto
    {
        Id = entity.Id,
        Model = entity.Model,
        SerialNumber = entity.SerialNumber,
    };
}
