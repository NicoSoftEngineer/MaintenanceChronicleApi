using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;

public class MaintenanceReminderInListForMachineDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public Instant Date { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceReminder"/> class.
/// </summary>
public static class MaintenanceReminderInListForMachineDtoExtension
{
    /// <summary>
    /// Convert <see cref="MaintenanceReminder"/> to <see cref="MaintenanceReminderInListForMachineDto"/>.
    /// </summary>
    /// <param name="mr"><see cref="MaintenanceReminder"/> entity</param>
    /// <returns>Mapped entity to <see cref="MaintenanceReminderInListForMachineDto"/></returns>
    public static MaintenanceReminderInListForMachineDto ToListDto(this MaintenanceReminder mr) => new MaintenanceReminderInListForMachineDto
    {
        Id = mr.Id,
        Description = mr.Description,
        Date = mr.Date,
    };
}
