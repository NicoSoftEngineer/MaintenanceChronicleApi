namespace ServiceTrack.Application.Contracts.Locations.Commands.Dto;

public class AssignContactsToLocationDto
{
    public Guid LocationId { get; set; }
    public Guid[] ContactIds { get; set; }
}
