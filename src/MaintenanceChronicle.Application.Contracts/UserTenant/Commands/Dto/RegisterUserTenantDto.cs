namespace MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;

public class RegisterUserTenantDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public required string TenantName { get; set; }
}
