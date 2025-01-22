using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading;
using MaintenanceChronicle.Api.Options;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Data.Entities.Business;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using NodaTime;

namespace MaintenanceChronicle.BackgroundServices.Services;

public class EmailSenderService(
    IClock clock,
    AppDbContext dbContext,
    IOptions<EnvironmentOptions> environmentOptions,
    IOptions<SmtpOptions> smtpClientOptions,
    IMediator mediator)
{
    private readonly SmtpOptions smtpOptions = smtpClientOptions.Value;
    private readonly EnvironmentOptions envOptions = environmentOptions.Value;

    public async Task SendEmailsAsync()
    {
        var mailQuery = new GetListOfEntityQuery<EmailMessageInListDto>();
        var mails = await mediator.Send(mailQuery);

        //TODO: create filtered query for mails
        var unsentMails = mails.Where(m => !m.Sent).ToList();

        foreach (var unsent in unsentMails)
        {
            using var mail = new MailMessage
            {
                Subject = unsent.Subject,
                Body = unsent.Body,
                IsBodyHtml = false,
                From = new MailAddress(unsent.FromEmail, unsent.FromName),
            };
            mail.To.Add(new MailAddress(unsent.RecipientEmail, unsent.RecipientName));

            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(smtpOptions.Host, smtpOptions.Port);
                await smtp.AuthenticateAsync(smtpOptions.Username, smtpOptions.Password);
                await smtp.SendAsync((MimeMessage)mail);

                unsent.Sent = true;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
