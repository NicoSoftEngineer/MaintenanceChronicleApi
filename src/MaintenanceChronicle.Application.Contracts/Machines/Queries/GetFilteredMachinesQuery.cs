using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries;

public record GetFilteredMachinesQuery(MachineFilterDto Filter) : IRequest<List<MachineInListDto>>;
