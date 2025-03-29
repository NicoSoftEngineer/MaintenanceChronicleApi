using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineDetailDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string Manufacture { get; set; }
    public required string  SerialNumber { get; set; }
    public required string Color { get; set; }
    public Instant InUseSince { get; set; }
}
/// <summary>
/// Extension methods for <see cref="Machine"/> entity.
/// </summary>
public static class MachineDetailDtoExtension
{
    /// <summary>
    /// Converts <see cref="Machine"/> to <see cref="MachineDetailDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="Machine"/> to convert</param>
    /// <returns>Converted entity to <see cref="MachineDetailDto"/></returns>
    public static MachineDetailDto ToMachineDetailDto(this Machine entity) => new MachineDetailDto
    {
        Id = entity.Id,
        Color = entity.Color,
        InUseSince = entity.InUseSince,
        Manufacture = entity.Manufacture,
        Model = entity.Model,
        SerialNumber = entity.SerialNumber
    };
}
