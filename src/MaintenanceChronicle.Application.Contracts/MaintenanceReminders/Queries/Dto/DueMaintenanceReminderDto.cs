using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;

public class DueMaintenanceReminderDto()
{
    public Guid Id { get; set; }
    public required string Description { get; set; } 
    public Instant SendAt { get; set; }
    public bool WasSent { get; set; }
    public Guid MachineId { get; set; }
}
public static class DueMaintenanceReminderDtoExtension
{
    public static DueMaintenanceReminderDto ToDto(this MaintenanceReminder mr) => new DueMaintenanceReminderDto
    {
        Id = mr.Id,
        Description = mr.Description,
        SendAt = mr.Date,
        MachineId = mr.MachineId,
        WasSent = mr.WasReminderSent,
    };
}
