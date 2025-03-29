using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;

/// <summary>
/// Command to create a new maintenance reminder for a machine
/// </summary>
/// <param name="Reminder">Data to create the Reminder by</param>
/// <param name="UserId">User, who created the MaintenanceReminder</param>
/// <param name="TenantId">Current tenant</param>
public record CreateNewMaintenanceReminderCommand(NewMaintenanceReminderDto Reminder, string UserId, string TenantId) : IRequest;
