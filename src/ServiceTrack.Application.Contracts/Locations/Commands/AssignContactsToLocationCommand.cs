using MediatR;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Locations.Commands;

public record AssignContactsToLocationCommand(AssignContactsToLocationDto ContactsToLocationDto, string UserId, string TenantId) : IRequest;
