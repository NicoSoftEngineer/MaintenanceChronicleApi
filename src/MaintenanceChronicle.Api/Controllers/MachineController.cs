using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
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
    /// <summary>
    /// Creates new machine
    /// </summary>
    /// <param name="newMachineDetail">Information from user</param>
    /// <returns>New machine id</returns>
    [HttpPost("/api/v1/machines")]
    public async Task<ActionResult<Guid>> CreateMachine([FromBody] NewMachineDto newMachineDetail)
    {
        var createCommand = new CreateNewMachineCommand(newMachineDetail, User.GetUserId(), User.GetTenantId());
        var machineId = await mediator.Send(createCommand);

        return Ok(machineId);
    }

    /// <summary>
    /// Gets machine by specified id
    /// </summary>
    /// <param name="id">Machine id specified by user</param>
    /// <returns>MachineDetailDto</returns>
    [HttpGet("/api/v1/machines/{id:guid}")]
    public async Task<ActionResult<MachineDetailDto>> GetMachineById([FromRoute] Guid id)
    {
        var machineQuery = new GetEntityByIdQuery<MachineDetailDto>(id);
        var machine = await mediator.Send(machineQuery);

        return Ok(machine);
    }

    /// <summary>
    /// Gets list of machines
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/v1/machines")]
    public async Task<ActionResult<List<MachineInListDto>>> GetMachineList()
    {
        var query = new GetListOfEntityQuery<MachineInListDto>();
        var machines = await mediator.Send(query);

        return Ok(machines);
    }
}
