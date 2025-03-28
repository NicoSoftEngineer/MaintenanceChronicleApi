using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceChronicle.Api.Controllers;

/// <summary>
/// Controller for managing maintenance reminders
/// </summary>
[ApiController]
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin},{RoleTypes.Technician}")]
[Route("api/v1/maintenance-reminders")]
public class MaintenanceReminderController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new maintenance reminder
    /// </summary>
    /// <param name="reminderDto">
    /// The new maintenance reminder data
    /// </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateMaintenanceReminder([FromBody] NewMaintenanceReminderDto reminderDto)
    {
        var command = new CreateNewMaintenanceReminderCommand(reminderDto, User.GetUserId(), User.GetTenantId());
        await mediator.Send(command);
        
        return Ok();
    }


}
