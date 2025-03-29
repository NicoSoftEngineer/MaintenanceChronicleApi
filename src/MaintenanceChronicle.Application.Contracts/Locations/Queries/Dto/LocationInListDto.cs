using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;

public class LocationInListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string CustomerName { get; set; }
}
/// <summary>
/// Extension methods for <see cref="LocationInListDto"/> entity.
/// </summary>
public static class LocationInListExtensions
{
    /// <summary>
    /// Converts <see cref="Location"/> to <see cref="LocationInListDto"/>.
    /// </summary>
    /// <param name="location"><see cref="Location"/> to convert</param>
    /// <returns>Converted entity to <see cref="LocationInListDto"/></returns>
    public static LocationInListDto ToLocationInListDto(this Location location)
    {
        return new LocationInListDto
        {
            Id = location.Id,
            Name = location.Name,
            Street = location.Street,
            City = location.City,
            Country = location.Country,
            CustomerName = location.Customer.Name
        };
    }
}
