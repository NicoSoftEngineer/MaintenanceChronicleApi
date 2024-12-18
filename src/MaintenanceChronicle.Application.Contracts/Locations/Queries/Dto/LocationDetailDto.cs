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

public static class LocationDetailDtoExtensions
{
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
