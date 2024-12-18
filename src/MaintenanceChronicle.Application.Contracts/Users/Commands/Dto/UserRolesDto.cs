namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class UserRolesDto
{
    public required Guid UserId { get; set; }
    public Guid[] RoleIds { get; set; }
}
