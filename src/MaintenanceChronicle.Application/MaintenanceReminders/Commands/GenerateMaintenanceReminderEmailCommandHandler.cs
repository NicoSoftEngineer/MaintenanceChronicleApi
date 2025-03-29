using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.MaintenanceReminders.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Account;
using MaintenanceChronicle.Utilities.EmailTemplates;
using MaintenanceChronicle.Utilities.Error;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.MaintenanceReminders.Commands;
/// <summary>
/// Handler for <see cref="GenerateMaintenanceReminderEmailCommand"/>.
/// </summary>
public class GenerateMaintenanceReminderEmailCommandHandler(AppDbContext dbContext, IOptions<EnvironmentOptions> envOptions) : IRequestHandler<GenerateMaintenanceReminderEmailCommand, NewEmailMessageDto>
{
    public async Task<NewEmailMessageDto> Handle(GenerateMaintenanceReminderEmailCommand request,
        CancellationToken cancellationToken)
    {
        // Get machine for reminder
        var machine = await dbContext.Machines
            .Include(m => m.Location)
                .ThenInclude(l => l.Contacts)
                    .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == request.Reminder.MachineId, cancellationToken: cancellationToken);
        if (machine == null)
        {
            throw new BadRequestException(ErrorType.MachineNotFound);
        }

        // Get date in format yyyy-MM-dd
        var date = InstantPattern.CreateWithInvariantCulture("yyyy-MM-dd").Format(request.Reminder.Date);

        // Get email body
        var emailHelper = new EmailTemplateHelper();
        var body = await emailHelper.GetMaintenanceReminderEmailTemplate($"{machine.Manufacture} {machine.Model}", machine.SerialNumber, date, request.Reminder.Description, machine.Location.Name, $"{machine.Location.Street}, {machine.Location.City}, {machine.Location.Country}");

        // Get users for email
        var users = machine.Location.Contacts.Select(c => c.User).ToList();
        // Create email message
        var emailMessage = new NewEmailMessageDto
        {
            Body = body,
            Subject = "Maintenance Reminder",
            Recipients = users.Select(u =>(u.Email!, $"{u.FirstName} {u.LastName}")).ToDictionary(),
            SendAt = date,
        };

        return emailMessage;
    }
}
