using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries;

/// <summary>
/// Gets list of all due maintenance reminders
/// </summary>
/// <returns>
/// List of all maintenance reminders that are due
/// </returns>
public record GetAllDueMaintenanceRemindersQuery() : IRequest<List<DueMaintenanceReminderDto>>;
