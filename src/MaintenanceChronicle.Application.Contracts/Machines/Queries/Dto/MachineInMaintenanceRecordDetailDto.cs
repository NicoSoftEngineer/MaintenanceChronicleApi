using MaintenanceChronicle.Data.Entities.Business;
using NodaTime.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

public static class MachineInMaintenanceRecordDetailExtension
{
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
