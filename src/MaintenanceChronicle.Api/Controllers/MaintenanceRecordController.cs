using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Account;
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
public class MaintenanceRecordController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates new MaintenanceRecord, that is assigned to defined machine
    /// </summary>
    /// <param name="recordDto">MaintenanceRecord with user defined info</param>
    /// <returns>New MaintenanceRecord id</returns>
    [HttpPost("/api/v1/maintenance-records/")]
    public async Task<ActionResult<Guid>> CreateMaintenanceRecord([FromBody] NewMaintenanceRecordDto recordDto)
    {
        var command = new CreateNewMaintenanceRecordCommand(recordDto, User.GetUserId(), User.GetTenantId());
        var recordId = await mediator.Send(command);

        return Ok(recordId);
    }

    [HttpPatch("/api/v1/maintenance-records/{id:guid}")]
    public async Task<ActionResult<ManageMaintenanceRecordDetailDto>> UpdateMaintenanceRecord([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<ManageMaintenanceRecordDetailDto> patch)
    {
        var command = new UpdateMaintenanceRecordCommand(patch, id, User.GetUserId());
        var result = await mediator.Send(command);

        return Ok(result);
    }
}
