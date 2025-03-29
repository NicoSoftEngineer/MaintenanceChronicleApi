using MaintenanceChronicle.Data.Entities.Account;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class UpdateUserDetailDto
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
}
/// <summary>
/// Extension methods for User entity.
/// </summary>
public static class UpdateUserDetailExtension
{
    /// <summary>
    /// Convert User entity to UpdateUserDetailDto.
    /// </summary>
    /// <param name="entity">Entity to convert</param>
    /// <returns>Converted entity</returns>
    public static UpdateUserDetailDto ToUpdateDetail(this User entity) => new UpdateUserDetailDto
    {
        Id = entity.Id,
        Email = entity.Email!,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        PhoneNumber = entity.PhoneNumber,
    };
    /// <summary>
    /// Maps UpdateUserDetailDto to User entity.
    /// </summary>
    /// <param name="from">Source UpdateUserDetailDto</param>
    /// <param name="to">Destination User</param>
    public static void MapToEntity(this UpdateUserDetailDto from, User to)
    {
        to.FirstName = from.FirstName;
        to.LastName = from.LastName;
        to.PhoneNumber = from.PhoneNumber;
    }
}
