using MaintenanceChronicle.Api.Options;
using MaintenanceChronicle.BackgroundServices.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MaintenanceChronicle.BackgroundServices.BackgroundWorkers;

public class EmailSenderBackgroundService(
    IServiceProvider provider)
    : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await SendEmails(cancellationToken);
    }

    private async Task SendEmails(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = provider.CreateScope();
            var emailSenderService = scope.ServiceProvider.GetRequiredService<EmailSenderService>();
            await emailSenderService.SendEmailsAsync();

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }
}
