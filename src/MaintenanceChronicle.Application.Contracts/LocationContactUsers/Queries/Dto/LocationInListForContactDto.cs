using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;

public class LocationInListForContactDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
}

public static class LocationInListForContactExtension
{
    public static LocationInListForContactDto ToListForContactDto(this LocationContactUser entity) => new LocationInListForContactDto
    {
        Name = entity.Location.Name,
        Address = $"{entity.Location.Street}, {entity.Location.City}"
    };
}
