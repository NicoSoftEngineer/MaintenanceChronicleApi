using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;

public class LocationInListForContactDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
}
/// <summary>
/// Extension methods for <see cref="LocationContactUser"/> entity.
/// </summary>
public static class LocationInListForContactExtension
{
    /// <summary>
    /// Converts <see cref="LocationContactUser"/> to <see cref="LocationInListForContactDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="LocationContactUser"/> to convert</param>
    /// <returns>Converted entity to <see cref="LocationInListForContactDto"/></returns>
    public static LocationInListForContactDto ToListForContactDto(this LocationContactUser entity) => new LocationInListForContactDto
    {
        Name = entity.Location.Name,
        Address = $"{entity.Location.Street}, {entity.Location.City}"
    };
}
