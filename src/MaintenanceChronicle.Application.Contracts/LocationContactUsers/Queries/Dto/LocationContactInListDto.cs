using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;

public class LocationContactInListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}
/// <summary>
/// Extension methods for <see cref="LocationContactInListDto"/> entity.
/// </summary>
public static class LocationContactInListDtoExtension
{
    /// <summary>
    /// Converts <see cref="LocationContactUser"/> to <see cref="LocationContactInListDto"/>.
    /// </summary>
    /// <param name="user"><see cref="LocationContactUser"/> to convert</param>
    /// <returns>Converted entity to <see cref="LocationContactInListDto"/></returns>
    public static LocationContactInListDto ToLocationContactInListDto(this
        LocationContactUser user) => new LocationContactInListDto
        {
            Id = user.UserId,
            Email = user.User.Email!,
            Name = $"{user.User.FirstName} {user.User.LastName}",
            PhoneNumber = user.User.PhoneNumber
        };

    /// <summary>
    /// Converts <see cref="User"/> to <see cref="LocationContactInListDto"/>.
    /// </summary>
    /// <param name="user"><see cref="User"/> to convert</param>
    /// <returns>Converted entity to <see cref="LocationContactInListDto"/></returns>
    public static LocationContactInListDto ToLocationContactInListDto(this
        User user) => new LocationContactInListDto
    {
        Id = user.Id,
        Email = user.Email!,
        Name = $"{user.FirstName} {user.LastName}",
        PhoneNumber = user.PhoneNumber
    };
}
