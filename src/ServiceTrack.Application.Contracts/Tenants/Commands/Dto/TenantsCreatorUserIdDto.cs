namespace ServiceTrack.Application.Contracts.Tenants.Commands.Dto;

public class TenantsCreatorUserIdDto
{
    public required Guid TenantId { get; set; }
    public required string UserId { get; set; }
}
