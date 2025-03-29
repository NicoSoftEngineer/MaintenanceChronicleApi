using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;

public class NewLocationDto
{
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public Guid CustomerId { get; set; }
}
/// <summary>
/// Extension methods for <see cref="NewLocationDto"/> entity.
/// </summary>
public static class NewLocationDtoExtension
{
    /// <summary>
    /// Converts <see cref="NewLocationDto"/> to <see cref="Location"/>.
    /// </summary>
    /// <param name="newLocationDto"><see cref="NewLocationDto"/> to convert</param>
    /// <returns>Converted entity to <see cref="Location"/></returns>
    public static Location ToEntity(this NewLocationDto newLocationDto)
    {
        return new Location
        {
            Name = newLocationDto.Name,
            Street = newLocationDto.Street,
            City = newLocationDto.City,
            Country = newLocationDto.Country,
            CustomerId = newLocationDto.CustomerId
        };
    }
}
