using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;

public class ManageMachineDetailDto
{
    public Guid Id { get; set; }
    public required string Model { get; set; }
    public required string Manufacture { get; set; }
    public required Guid LocationId { get; set; }
    public required string SerialNumber { get; set; }
    public required string Color { get; set; }
    public required string InUseSince { get; set; }
}
/// <summary>
/// Extension methods for <see cref="ManageMachineDetailDto"/> entity.
/// </summary>
public static class ManageMachineDetailExtension
{
    /// <summary>
    /// Converts <see cref="Machine"/> to <see cref="ManageMachineDetailDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="Machine"/> to convert</param>
    /// <returns>Converted entity to <see cref="ManageMachineDetailDto"/></returns>
    public static ManageMachineDetailDto ToManageMachineDetailDto(this Machine entity) => new ManageMachineDetailDto
    {
        Id = entity.Id,
        Color = entity.Color,
        InUseSince = entity.InUseSince.ToString(),
        Manufacture = entity.Manufacture,
        Model = entity.Model,
        SerialNumber = entity.SerialNumber,
        LocationId = entity.LocationId,
    };

    /// <summary>
    /// Maps <see cref="ManageMachineDetailDto"/> properties to <see cref="Machine"/>.
    /// </summary>
    /// <param name="dto">Source <see cref="ManageMachineDetailDto"/></param>
    /// <param name="target">Destination <see cref="Machine"/></param>
    public static void MapToEntity(this ManageMachineDetailDto dto, Machine target)
    {
        target.Model = dto.Model;
        target.Color = dto.Color;
        target.Manufacture = dto.Manufacture;
        target.SerialNumber = dto.SerialNumber;
        target.InUseSince = InstantPattern.General.Parse(dto.InUseSince.Split("T")[0] + "T12:00:00Z").Value;
        target.LocationId = dto.LocationId;
    }
}
