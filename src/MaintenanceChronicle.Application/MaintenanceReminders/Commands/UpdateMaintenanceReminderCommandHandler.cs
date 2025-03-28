using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Interfaces;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Commands;

public class UpdateMaintenanceReminderCommandHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<UpdateMaintenanceReminderCommand, MaintenanceReminderDetailDto>
{
    public async Task<MaintenanceReminderDetailDto> Handle(UpdateMaintenanceReminderCommand request, CancellationToken cancellationToken)
    {
        var maintenanceReminder = await dbContext.MaintenanceReminders
            .FindAsync([request.Id], cancellationToken);
        if (maintenanceReminder == null)
        {
            throw new BadRequestException(ErrorType.MaintenanceReminderNotFound);
        }

        var dto = maintenanceReminder.ToDto();
        request.Patch.ApplyTo(dto);
        dto.MapToEntity(maintenanceReminder);

        maintenanceReminder.SetModifyBy(request.UserId, clock.GetCurrentInstant());

        await dbContext.SaveChangesAsync(cancellationToken);
        return dto;
    }
}
