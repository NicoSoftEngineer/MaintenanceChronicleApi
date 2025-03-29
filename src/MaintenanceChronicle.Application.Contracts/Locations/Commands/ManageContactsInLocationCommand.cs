using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;
/// <summary>
/// Command to manage contacts in a location.
/// </summary>
/// <param name="LocationId">ID of location where the contacts belong</param>
/// <param name="Contacts"><see cref="LocationContactInListDto"/> array with the contacts for location</param>
/// <param name="UserId">ID of requesting user</param>
/// <param name="TenantId">ID of responsible tenant</param>
public record ManageContactsInLocationCommand(
    Guid LocationId,
    LocationContactInListDto[] Contacts,
    string UserId,
    string TenantId) : IRequest;
