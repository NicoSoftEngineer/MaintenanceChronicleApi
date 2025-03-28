using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;

public record GenerateMaintenanceReminderEmailCommand(NewMaintenanceReminderDto Reminder) : IRequest<NewEmailMessageDto>;
