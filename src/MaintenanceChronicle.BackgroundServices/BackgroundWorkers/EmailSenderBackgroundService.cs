using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MaintenanceChronicle.BackgroundServices.BackgroundWorkers;

public class EmailSenderBackgroundService(
    IServiceProvider provider)
    : BackgroundService
{
    /// <summary>
    /// Function definition from BackgroundService, which gets called at the start of an app
    /// Calls private SendEmails
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await SendEmails(cancellationToken);
    }

    /// <summary>
    /// Has infinite loop, which gets all emails, and then which where unsent get passed into SendEmailCommand where they get sent
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task SendEmails(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = provider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var emailQuery = new GetListOfEntityQuery<EmailMessageInListDto>();
            var emails = await mediator.Send(emailQuery, cancellationToken);

            foreach (var unsentEmailMessage in emails.Where(x => !x.Sent))
            {
                var sendEmailCommand = new SendEmailMessageCommand(unsentEmailMessage.Id);
                await mediator.Send(sendEmailCommand, cancellationToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }
}
