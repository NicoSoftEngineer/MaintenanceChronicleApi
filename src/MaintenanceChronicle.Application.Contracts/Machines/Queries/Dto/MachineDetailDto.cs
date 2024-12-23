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

public static class MachineDetailDtoExtension
{
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
