using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;

public class MaintenanceReminderInListForMachineDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
}
public static class MaintenanceReminderInListForMachineDtoExtension
{
    public static MaintenanceReminderInListForMachineDto ToListDto(this MaintenanceReminder mr) => new MaintenanceReminderInListForMachineDto
    {
        Id = mr.Id,
        Description = mr.Description,
        Date = mr.Date,
    };
}
