namespace ServiceTrack.Application.Contracts.Users.Commands.Dto;

public class RegisterUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Guid TenantId { get; set; }
}
