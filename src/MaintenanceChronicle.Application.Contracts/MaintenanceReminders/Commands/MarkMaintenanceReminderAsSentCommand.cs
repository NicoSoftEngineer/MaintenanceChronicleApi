using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;

public record MarkMaintenanceReminderAsSentCommand(Guid ReminderId) : IRequest;
