using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands;

public record UpdateMachineCommand(JsonPatchDocument<ManageMachineDetailDto> Patch, Guid MachineId, string UserId)
    : IRequest<ManageMachineDetailDto>;
