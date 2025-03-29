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
/// <summary>
/// Extension methods for <see cref="NewMachineDto"/> entity.
/// </summary>
public static class NewMachineDtoExtension
{
    /// <summary>
    /// Converts <see cref="NewMachineDto"/> to <see cref="Machine"/>.
    /// </summary>
    /// <param name="newMachineDto"><see cref="NewMachineDto"/> to convert</param>
    /// <returns>Converted entity to <see cref="Machine"/></returns>
    public static Machine ToMachineEntity(this NewMachineDto newMachineDto) => new Machine
    {
        Model = newMachineDto.Model,
        Manufacture = newMachineDto.Manufacture,
        Color = newMachineDto.Color,
        SerialNumber = newMachineDto.SerialNumber,
        InUseSince = InstantPattern.General.Parse(newMachineDto.InUseSince.Split("T")[0] + "T12:00:00Z").Value,
        LocationId = newMachineDto.LocationId,
    };
}
