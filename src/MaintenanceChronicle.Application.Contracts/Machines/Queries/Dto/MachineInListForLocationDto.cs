using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineInListForLocationDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string SerialNumber { get; set; }
}

public static class MachineInListForLocationExtension
{
    public static MachineInListForLocationDto ToMachineInListForLocationDto(this Machine entity) => new MachineInListForLocationDto
    {
        Id = entity.Id,
        Model = entity.Model,
        SerialNumber = entity.SerialNumber,
    };
}
