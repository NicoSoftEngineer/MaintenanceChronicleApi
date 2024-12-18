using MediatR;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Locations.Commands;

public record ManageContactsInLocationCommand(
    Guid LocationId,
    ContactsInLocationDto ContactsInLocationDto,
    string UserId,
    string TenantId) : IRequest;
