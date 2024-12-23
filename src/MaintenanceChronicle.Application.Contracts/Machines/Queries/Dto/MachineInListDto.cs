using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineInListDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string LocationName { get; set; }
}

public static  class MachineInListExtension
{
    public static MachineInListDto ToMachineInListDto(this Machine entity) => new MachineInListDto
    {
        Id = entity.Id,
        Model = entity.Model,
        LocationName = entity.Location.Name,
    };
}
