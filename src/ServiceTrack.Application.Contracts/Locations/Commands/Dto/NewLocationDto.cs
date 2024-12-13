using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Data.Entities.Business;

namespace ServiceTrack.Application.Contracts.Locations.Commands.Dto;

public class NewLocationDto
{
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public Guid CustomerId { get; set; }
}
public static class NewLocationDtoExtension
{

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
