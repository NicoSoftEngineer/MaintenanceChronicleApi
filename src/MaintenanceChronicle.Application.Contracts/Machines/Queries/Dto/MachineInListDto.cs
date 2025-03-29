using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineInListDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string LocationName { get; set; }
    public required string CustomerName { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MachineInListDto"/> entity.
/// </summary>
public static  class MachineInListExtension
{
    /// <summary>
    /// Converts <see cref="Machine"/> to <see cref="MachineInListDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="Machine"/> to convert</param>
    /// <returns>Converted entity to <see cref="MachineInListDto"/></returns>
    public static MachineInListDto ToMachineInListDto(this Machine entity) => new MachineInListDto
    {
        Id = entity.Id,
        Model = entity.Model,
        LocationName = entity.Location.Name,
        CustomerName = entity.Location.Customer.Name
    };
}
