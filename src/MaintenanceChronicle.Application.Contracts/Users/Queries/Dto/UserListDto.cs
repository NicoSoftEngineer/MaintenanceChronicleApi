using MaintenanceChronicle.Application.Contracts.Roles.Dto;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;

public class UserListDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public List<RoleDetailDto> Roles { get; set; } = new ();
}
