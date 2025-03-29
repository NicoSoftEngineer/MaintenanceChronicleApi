using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
/// <summary>
/// Command for updating maintenance reminder.
/// </summary>
/// <param name="Id">ID of reminder that should get reminded</param>
/// <param name="Patch">JsonPatchDocument with instructions on what to replace</param>
/// <param name="UserId">ID of user that requested the update</param>
/// <returns> <see cref="MaintenanceReminderDetailDto"/> with updated information</returns>
public record UpdateMaintenanceReminderCommand(Guid Id, JsonPatchDocument<MaintenanceReminderDetailDto> Patch, string UserId) : IRequest<MaintenanceReminderDetailDto>;
