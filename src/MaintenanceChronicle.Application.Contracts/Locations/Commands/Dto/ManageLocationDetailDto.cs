using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;

public class ManageLocationDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public Guid CustomerId { get; set; }
}

public static class ManageLocationDetailDtoExtension
{
    public static ManageLocationDetailDto ToManageDto(this Location location)
    {
        return new ManageLocationDetailDto
        {
            Id = location.Id,
            Name = location.Name,
            Street = location.Street,
            City = location.City,
            Country = location.Country,
            CustomerId = location.CustomerId
        };
    }
    public static void MapToEntity(this ManageLocationDetailDto manageLocationDetailDto, Location target)
    {
        target.Id = manageLocationDetailDto.Id;
        target.Name = manageLocationDetailDto.Name;
        target.Street = manageLocationDetailDto.Street;
        target.City = manageLocationDetailDto.City;
        target.Country = manageLocationDetailDto.Country;
        target.CustomerId = manageLocationDetailDto.CustomerId;
    }
}
