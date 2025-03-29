using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;
/// <summary>
/// Command to create a new location.
/// </summary>
/// <param name="LocationDto"><see cref="NewLocationDto"/> with data</param>
/// <param name="UserId">ID of requesting user</param>
/// <param name="TenantId">ID of responsible tenant</param>
public record CreateNewLocationCommand(NewLocationDto LocationDto, string UserId, string TenantId) : IRequest<Guid>;
