using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries;
/// <summary>
/// Query to get all maintenance reminders for a machine.
/// </summary>
/// <param name="MachineId">The machine ID</param>
/// <returns>The list with all the Reminders</returns>
public record GetAllMaintenanceRemindersForMachineQuery(Guid MachineId) : IRequest<List<MaintenanceReminderInListForMachineDto>>;
