namespace MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;

public class ContactsInLocationDto
{
    public Guid LocationId { get; set; }
    public required Guid[] ContactIds { get; set; }
}
