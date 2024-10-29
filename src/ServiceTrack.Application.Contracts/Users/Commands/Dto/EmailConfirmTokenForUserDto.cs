namespace ServiceTrack.Application.Contracts.Users.Commands.Dto;

public class EmailConfirmTokenForUserDto
{
    public required string Email { get; set; }
    public required string Token { get; set; }
}
