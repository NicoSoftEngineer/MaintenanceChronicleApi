namespace ServiceTrack.Application.Contracts.Tenants.Queries.Dto;

public class TenantDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
