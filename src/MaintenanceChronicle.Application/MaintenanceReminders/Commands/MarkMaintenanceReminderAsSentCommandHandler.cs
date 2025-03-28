using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Data;
using MediatR;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Commands;

public class MarkMaintenanceReminderAsSentCommandHandler(AppDbContext dbContext) : IRequestHandler<MarkMaintenanceReminderAsSentCommand>
{
    public async Task Handle(MarkMaintenanceReminderAsSentCommand request, CancellationToken cancellationToken)
    {
        var maintenanceReminder = await dbContext.MaintenanceReminders
            .FindAsync([request.ReminderId ], cancellationToken);

        if (maintenanceReminder is not null)
        {
            maintenanceReminder.WasReminderSent = true;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
