namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class AddPasswordToRegisteredUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
