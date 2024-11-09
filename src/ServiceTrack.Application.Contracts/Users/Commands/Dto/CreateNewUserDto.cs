namespace ServiceTrack.Application.Contracts.Users.Commands.Dto;

public class CreateNewUserDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
