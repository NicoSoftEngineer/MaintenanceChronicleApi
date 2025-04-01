using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;

public class MaintenanceReminderDetailDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = String.Empty;
    public required string Date { get; set; }
    public Guid MachineId { get; set; }
}
/// <summary>
/// Extension methods for <see cref="MaintenanceReminderDetailDto"/>.
/// </summary>
public static class MaintenanceReminderDetailDtoExtension
{
    /// <summary>
    /// Convert <see cref="MaintenanceReminder"/> to <see cref="MaintenanceReminderDetailDto"/>.
    /// </summary>
    /// <param name="mr"><see cref="MaintenanceReminder"/> entity to convert</param>
    /// <returns>Converted entity to  <see cref="MaintenanceReminderDetailDto"/></returns>
    public static MaintenanceReminderDetailDto ToDto(this MaintenanceReminder mr) => new MaintenanceReminderDetailDto
    {
        Id = mr.Id,
        Description = mr.Description,
        Date = mr.Date.ToString(),
        MachineId = mr.MachineId,
    };
    /// <summary>
    /// Assigns values from <see cref="MaintenanceReminderDetailDto"/> to <see cref="MaintenanceReminder"/>.
    /// </summary>
    /// <param name="dto">The source <see cref="MaintenanceReminderDetailDto"/></param>
    /// <param name="entity">The destination <see cref="MaintenanceReminder"/></param>
    public static void MapToEntity(this MaintenanceReminderDetailDto dto, MaintenanceReminder entity)
    {
        entity.Description = dto.Description;
        entity.Date = InstantPattern.General.Parse(dto.Date).Value;
        entity.MachineId = dto.MachineId;
    }
}
