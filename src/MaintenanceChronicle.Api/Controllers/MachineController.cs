using MaintenanceChronicle.Application.Contracts.Machines.Commands;
using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Commands;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Application.Machines.Commands;
using MaintenanceChronicle.Data.Entities.Business;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    /// Updates machine info
    /// </summary>
    /// <param name="id">Updated machine id</param>
    /// <param name="patch">Object that says what props should be changed and how</param>
    /// <returns>Updated MachineDetail</returns>
    [HttpPatch("/api/v1/machines/{id:guid}")]
    public async Task<ActionResult<ManageMachineDetailDto>> UpdateMachine([FromRoute] Guid id, [FromBody] JsonPatchDocument<ManageMachineDetailDto> patch)
    {
        var command = new UpdateMachineCommand(patch, id, User.GetUserId());
        var result = await mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Soft deletes machine by specified id
    /// </summary>
    /// <param name="id">User specified machine id</param>
    /// <returns></returns>
    [HttpDelete("/api/v1/machines/{id:guid}")]
    public async Task<ActionResult> DeleteMachine([FromRoute] Guid id)
    {
        var command = new DeleteEntityByIdCommand<Machine>(id, User.GetUserId());
        await mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Gets machine by specified id
    /// </summary>
    /// <param name="id">Machine id specified by user</param>
    /// <returns>Patch</returns>
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
