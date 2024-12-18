using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;

public class LocationInListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}

public static class LocationInListExtensions
{
    public static LocationInListDto ToLocationInListDto(this Location location)
    {
        return new LocationInListDto
        {
            Id = location.Id,
            Name = location.Name,
            Street = location.Street,
            City = location.City,
            Country = location.Country
        };
    }
}
