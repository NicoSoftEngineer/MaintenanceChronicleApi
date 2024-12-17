using ServiceTrack.Data.Entities.Business;

namespace ServiceTrack.Application.Contracts.LocationContactUsers.Queries.Dto;

public class LocationContactInListDto
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public static class LocationContactInListDtoExtension
{
    public static LocationContactInListDto ToLocationContactInListDto(this
        LocationContactUser user) => new LocationContactInListDto
        {
            Id = user.UserId,
            Email = user.User.Email!,
            FullName = $"{user.User.FirstName} {user.User.LastName}",
            PhoneNumber = user.User.PhoneNumber
        };
}
