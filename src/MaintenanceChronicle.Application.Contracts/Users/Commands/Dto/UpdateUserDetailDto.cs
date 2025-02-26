using MaintenanceChronicle.Data.Entities.Account;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class UpdateUserDetailDto
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public static class UpdateUserDetailExtension
{
    public static UpdateUserDetailDto ToUpdateDetail(this User entity) => new UpdateUserDetailDto
    {
        Id = entity.Id,
        Email = entity.Email!,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
    };

    public static void MapToEntity(this UpdateUserDetailDto from, User to)
    {
        to.FirstName = from.FirstName;
        to.LastName = from.LastName;
    }
}
