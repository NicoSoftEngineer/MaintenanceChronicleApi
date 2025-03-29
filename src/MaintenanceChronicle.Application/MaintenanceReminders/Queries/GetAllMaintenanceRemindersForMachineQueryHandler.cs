using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Queries;
/// <summary>
/// Handler for <see cref="GetAllMaintenanceRemindersForMachineQuery"/>.
/// </summary>
public class GetAllMaintenanceRemindersForMachineQueryHandler(AppDbContext dbContext) : IRequestHandler<GetAllMaintenanceRemindersForMachineQuery, List<MaintenanceReminderInListForMachineDto>>
{
    public async Task<List<MaintenanceReminderInListForMachineDto>> Handle(GetAllMaintenanceRemindersForMachineQuery request, CancellationToken cancellationToken)
    {
        var maintenanceReminders = await dbContext.MaintenanceReminders
            .Include(mr => mr.Machine)
            .Where(mr => mr.MachineId == request.MachineId)
            .Select(mr => mr.ToListDto())
            .ToListAsync(cancellationToken);
        return maintenanceReminders;
    }
}
