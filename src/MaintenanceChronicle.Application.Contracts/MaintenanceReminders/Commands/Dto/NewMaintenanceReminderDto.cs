using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;

public class NewMaintenanceReminderDto
{
    public Guid MachineId { get; set; }
    public required string Date { get; set; }
    public required string Description { get; set; }
}

public static class NewMrExtension
{
    /// <summary>
    /// Maps the NewMaintenanceReminderDto to MaintenanceReminder entity
    /// </summary>
    /// <param name="dto">The specified dto to be mapped</param>
    /// <returns>Mapped entity</returns>
    public static MaintenanceReminder ToEntity(this NewMaintenanceReminderDto dto) => new MaintenanceReminder
    {
        MachineId = dto.MachineId,
        Date = InstantPattern.General.Parse(dto.Date.Split("T")[0] + "T12:00:00Z").Value,
        Description = dto.Description
    };
}
