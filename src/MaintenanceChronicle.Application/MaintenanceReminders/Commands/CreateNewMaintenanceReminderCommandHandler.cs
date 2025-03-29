using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Commands;

public class CreateNewMaintenanceReminderCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<CreateNewMaintenanceReminderCommand>
{
    public async Task Handle(CreateNewMaintenanceReminderCommand request, CancellationToken cancellationToken)
    {
        var machine = await dbContext.Machines.FindAsync([request.Reminder.MachineId], cancellationToken);
        if (machine == null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        var reminderEntity = request.Reminder.ToEntity();
        reminderEntity.TenantId = Guid.Parse(request.TenantId);
        reminderEntity.SetCreateBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.AddAsync(reminderEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
