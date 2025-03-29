using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Data;
using MediatR;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Commands;
/// <summary>
/// Handler for <see cref="MarkMaintenanceReminderAsSentCommand"/>
/// </summary>
public class MarkMaintenanceReminderAsSentCommandHandler(AppDbContext dbContext) : IRequestHandler<MarkMaintenanceReminderAsSentCommand>
{
    public async Task Handle(MarkMaintenanceReminderAsSentCommand request, CancellationToken cancellationToken)
    {
        // Find the reminder in the database
        var maintenanceReminder = await dbContext.MaintenanceReminders
            .FindAsync([request.ReminderId ], cancellationToken);

        // If the reminder is found, mark it as sent
        if (maintenanceReminder is not null)
        {
            maintenanceReminder.WasReminderSent = true;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
