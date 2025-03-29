using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;

public class ManageLocationDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
/// <summary>
/// Extension methods for <see cref="ManageLocationDetailDto"/> entity.
/// </summary>
public static class ManageLocationDetailDtoExtension
{
    /// <summary>
    /// Converts <see cref="Location"/> to <see cref="ManageLocationDetailDto"/>.
    /// </summary>
    /// <param name="location"><see cref="Location"/> to convert</param>
    /// <returns>Converted entity to <see cref="ManageLocationDetailDto"/></returns>
    public static ManageLocationDetailDto ToManageDto(this Location location)
    {
        return new ManageLocationDetailDto
        {
            Id = location.Id,
            Name = location.Name,
            Street = location.Street,
            City = location.City,
            Country = location.Country,
        };
    }
    /// <summary>
    /// Maps the props of <see cref="ManageLocationDetailDto"/> to <see cref="Location"/>.
    /// </summary>
    /// <param name="manageLocationDetailDto">Source <see cref="ManageLocationDetailDto"/></param>
    /// <param name="target">Destination <see cref="Location"/></param>
    public static void MapToEntity(this ManageLocationDetailDto manageLocationDetailDto, Location target)
    {
        target.Id = manageLocationDetailDto.Id;
        target.Name = manageLocationDetailDto.Name;
        target.Street = manageLocationDetailDto.Street;
        target.City = manageLocationDetailDto.City;
        target.Country = manageLocationDetailDto.Country;
    }
}
