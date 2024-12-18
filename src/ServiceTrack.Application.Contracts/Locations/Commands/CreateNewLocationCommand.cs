using MediatR;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Locations.Commands;

public record CreateNewLocationCommand(NewLocationDto LocationDto, string UserId, string TenantId) : IRequest<Guid>;
