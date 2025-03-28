using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries;

public record GetAllMaintenanceRemindersForMachineQuery(Guid MachineId) : IRequest<List<MaintenanceReminderInListForMachineDto>>;
