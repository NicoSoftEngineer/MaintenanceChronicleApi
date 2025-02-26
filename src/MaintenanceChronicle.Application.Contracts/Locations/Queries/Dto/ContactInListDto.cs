namespace MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;

public class ContactInListDto
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}
