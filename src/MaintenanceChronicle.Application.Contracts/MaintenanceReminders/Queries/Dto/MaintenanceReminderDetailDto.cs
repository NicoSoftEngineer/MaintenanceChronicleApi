using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;

public class MaintenanceReminderDetailDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Instant Date { get; set; }
    public Guid MachineId { get; set; }
}
public static class MaintenanceReminderDetailDtoExtension
{
    public static MaintenanceReminderDetailDto ToDto(this MaintenanceReminder mr) => new MaintenanceReminderDetailDto
    {
        Id = mr.Id,
        Description = mr.Description,
        Date = mr.Date,
        MachineId = mr.MachineId,
    };

    public static void MapToEntity(this MaintenanceReminderDetailDto dto, MaintenanceReminder entity)
    {
        entity.Description = dto.Description;
        entity.Date = dto.Date;
        entity.MachineId = dto.MachineId;
    }
}
