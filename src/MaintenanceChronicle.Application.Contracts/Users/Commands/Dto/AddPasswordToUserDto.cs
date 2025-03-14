namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class AddPasswordToUserDto
{
    public required string PasswordToken { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
