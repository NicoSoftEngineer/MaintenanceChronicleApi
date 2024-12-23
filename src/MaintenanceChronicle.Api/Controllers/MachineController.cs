using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceChronicle.Api.Controllers;

//Makes endpoints accessible only for users with Admin or GlobalAdmin roles
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin}")]
[ApiController]
public class MachineController(IMediator mediator) : ControllerBase
{
    [HttpPost("/api/v1/machines")]
    public async Task<ActionResult<Guid>> CreateMachine([FromBody] NewMachineDto newMachineDetail)
    {
        var createCommand = new CreateNewMachineCommand(newMachineDetail, User.GetUserId(), User.GetTenantId());
        var machineId = await mediator.Send(createCommand);

        return Ok(machineId);
    }
}
