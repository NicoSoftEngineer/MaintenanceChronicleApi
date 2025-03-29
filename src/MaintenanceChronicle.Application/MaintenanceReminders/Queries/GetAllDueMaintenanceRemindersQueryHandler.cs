using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Queries;
/// <summary>
/// Handler for <see cref="GetAllDueMaintenanceRemindersQuery"/>.
/// </summary>
public class GetAllDueMaintenanceRemindersQueryHandler(AppDbContext dbContext, IClock clock) : IRequestHandler<GetAllDueMaintenanceRemindersQuery, List<DueMaintenanceReminderDto>>
{
    public async Task<List<DueMaintenanceReminderDto>> Handle(GetAllDueMaintenanceRemindersQuery request, CancellationToken cancellationToken)
    {
        var dueMaintenanceReminders = await dbContext.MaintenanceReminders
            .Include(mr => mr.Machine)
            .Where(mr => mr.Date <= clock.GetCurrentInstant())
            .Select(mr => mr.ToDueDto())
            .ToListAsync(cancellationToken);

        return dueMaintenanceReminders;
    }
}
