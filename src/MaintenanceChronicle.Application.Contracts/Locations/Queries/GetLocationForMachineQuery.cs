using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries;

public record GetLocationForMachineQuery(Guid MachineId) : IRequest<LocationInListDto>;
