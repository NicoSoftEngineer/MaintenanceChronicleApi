using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
/// <summary>
/// Command to mark maintenance reminder as sent.
/// </summary>
/// <param name="ReminderId">ID of reminder to mark as sent</param>
public record MarkMaintenanceReminderAsSentCommand(Guid ReminderId) : IRequest;
