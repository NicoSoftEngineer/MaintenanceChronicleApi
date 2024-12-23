using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;

public class NewMachineDto
{
    public required string Model { get; set; }
    public required string Manufacture { get; set; }
    public required Guid LocationId { get; set; }
    public required string SerialNumber { get; set; }
    public required string Color { get; set; }
    public required string InUseSince { get; set; }
}

public static class NewMachineDtoExtension
{
    public static Machine ToMachineEntity(this NewMachineDto newMachineDto) => new Machine
    {
        Model = newMachineDto.Model,
        Manufacture = newMachineDto.Manufacture,
        Color = newMachineDto.Color,
        SerialNumber = newMachineDto.SerialNumber,
        InUseSince = InstantPattern.General.Parse(newMachineDto.InUseSince).Value,
        LocationId = newMachineDto.LocationId,
    };
}
