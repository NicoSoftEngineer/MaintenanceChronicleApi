using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.UserTenant.Commands.Dto;

public class UserTenantDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string TenantName { get; set; }
}
