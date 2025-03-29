using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;

public class LocationDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
/// <summary>
/// Extension methods for <see cref="LocationDetailDto"/> entity.
/// </summary>
public static class LocationDetailDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Location"/> to <see cref="LocationDetailDto"/>.
    /// </summary>
    /// <param name="location"><see cref="Location"/> to convert</param>
    /// <returns>Converted entity to <see cref="LocationDetailDto"/></returns>
    public static LocationDetailDto ToLocationDetailDto(this Location location)
    {
        return new LocationDetailDto
        {
            Id = location.Id,
            Name = location.Name,
            Street = location.Street,
            City = location.City,
            Country = location.Country
        };
    }
}
