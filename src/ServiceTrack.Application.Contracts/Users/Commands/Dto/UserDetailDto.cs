namespace ServiceTrack.Application.Contracts.Users.Commands.Dto;

public class UserDetailDto
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
