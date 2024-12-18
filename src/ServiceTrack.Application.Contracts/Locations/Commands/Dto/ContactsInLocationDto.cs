namespace ServiceTrack.Application.Contracts.Locations.Commands.Dto;

public class ContactsInLocationDto
{
    public Guid LocationId { get; set; }
    public Guid[] ContactIds { get; set; }
}
