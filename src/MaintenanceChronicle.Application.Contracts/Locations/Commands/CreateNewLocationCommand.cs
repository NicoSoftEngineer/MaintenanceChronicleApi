using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;

public record CreateNewLocationCommand(NewLocationDto LocationDto, string UserId, string TenantId) : IRequest<Guid>;
