using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;

public record ManageContactsInLocationCommand(
    Guid LocationId,
    ContactsInLocationDto ContactsInLocationDto,
    string UserId,
    string TenantId) : IRequest;
