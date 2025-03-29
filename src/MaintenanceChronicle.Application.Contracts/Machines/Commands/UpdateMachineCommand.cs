using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands;
/// <summary>
/// Command to update machine.
/// </summary>
/// <param name="Patch"><see cref="JsonPatchDocument"/> with instructions on what to replace</param>
/// <param name="MachineId">ID of machine that should get updated</param>
/// <param name="UserId">ID of requesting user</param>
/// <returns><see cref="ManageMachineDetailDto"/> of updated machine</returns>
public record UpdateMachineCommand(JsonPatchDocument<ManageMachineDetailDto> Patch, Guid MachineId, string UserId)
    : IRequest<ManageMachineDetailDto>;
