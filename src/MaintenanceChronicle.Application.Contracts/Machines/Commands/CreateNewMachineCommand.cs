using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands;

public record CreateNewMachineCommand(NewMachineDto NewMachineDto, string UserId, string TenantId) : IRequest<Guid>;
