using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;

public record ManageContactsInLocationCommand(
    Guid LocationId,
    ContactInListDto[] Contacts,
    string UserId,
    string TenantId) : IRequest;
