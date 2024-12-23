using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries;

public record GetMachinesForLocationQuery(Guid LocationId) : IRequest<List<MachineInListForLocationDto>>;
