using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Commands;
/// <summary>
/// Command to create a new machine.
/// </summary>
/// <param name="NewMachineDto"><see cref="NewMachineDto"/> with data</param>
/// <param name="UserId">ID of user who created the machine</param>
/// <param name="TenantId">ID of responsible tenant</param>
/// <returns>ID of created machine</returns>
public record CreateNewMachineCommand(NewMachineDto NewMachineDto, string UserId, string TenantId) : IRequest<Guid>;
