namespace ServiceTrack.Application.Contracts.Tenants.Commands.Dto;

public class UserTenantClaimDto
{
    public required string Email { get; set; }
    public required Guid TenantId { get; set; }
}
