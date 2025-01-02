namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class CreateNewUserDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Guid[] Roles { get; set; } = null!;
}
