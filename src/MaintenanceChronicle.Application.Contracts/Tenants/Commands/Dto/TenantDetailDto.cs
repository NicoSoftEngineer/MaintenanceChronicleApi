namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;

public class TenantDetailDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
