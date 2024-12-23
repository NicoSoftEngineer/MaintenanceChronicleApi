using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;

public class ManageMachineDetailDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string Manufacture { get; set; }
    public required Guid LocationId { get; set; }
    public required string SerialNumber { get; set; }
    public required string Color { get; set; }
    public required Instant InUseSince { get; set; }
}

public static class ManageMachineDetailExtension
{
    public static ManageMachineDetailDto ToManageMachineDetailDto(this Machine entity) => new ManageMachineDetailDto
    {
        Id = entity.Id,
        Color = entity.Color,
        InUseSince = entity.InUseSince,
        Manufacture = entity.Manufacture,
        Model = entity.Model,
        SerialNumber = entity.SerialNumber,
        LocationId = entity.LocationId,
    };

    public static void MapToEntity(this ManageMachineDetailDto dto, Machine target)
    {
        target.Model = dto.Model;
        target.Color = dto.Color;
        target.Manufacture = dto.Manufacture;
        target.SerialNumber = dto.SerialNumber;
        target.InUseSince = dto.InUseSince;
        target.LocationId = dto.LocationId;
    }
}
