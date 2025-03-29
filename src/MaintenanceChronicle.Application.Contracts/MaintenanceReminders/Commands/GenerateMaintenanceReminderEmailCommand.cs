using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
/// <summary>
/// Command to generate maintenance reminder email.
/// </summary>
/// <param name="Reminder">Dto of reminder for which the email is generated</param>
/// <returns> <see cref="NewEmailMessageDto"/> with reminder body</returns>
public record GenerateMaintenanceReminderEmailCommand(DueMaintenanceReminderDto Reminder) : IRequest<NewEmailMessageDto>;
