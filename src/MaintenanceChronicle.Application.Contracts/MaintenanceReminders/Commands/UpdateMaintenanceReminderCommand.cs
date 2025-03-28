using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;

public record UpdateMaintenanceReminderCommand(Guid Id, JsonPatchDocument<MaintenanceReminderDetailDto> Patch, string UserId) : IRequest<MaintenanceReminderDetailDto>;
