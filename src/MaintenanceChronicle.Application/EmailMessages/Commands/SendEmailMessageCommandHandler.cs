using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Error;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using MaintenanceChronicle.Utilities.Options;

namespace MaintenanceChronicle.Application.EmailMessages.Commands;

public class SendEmailMessageCommandHandler(AppDbContext dbContext, IOptions<SmtpOptions> smtpOptions) : IRequestHandler<SendEmailMessageCommand>
{
    public async Task Handle(SendEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var emailMessage = await dbContext.EmailMessages.FindAsync([request.MessageId], cancellationToken);
        if (emailMessage == null)
        {
            throw new BadRequestException(ErrorType.EmailMessageNotFound);
        }

        if (emailMessage.Sent)
        {
            throw new BadRequestException(ErrorType.EmailAlreadySent);
        }

        using var mail = new MailMessage
        {
            Subject = emailMessage.Subject,
            Body = emailMessage.Body,
            IsBodyHtml = false,
            From = new MailAddress(emailMessage.FromEmail, emailMessage.FromName),
        };
        mail.To.Add(new MailAddress(emailMessage.RecipientEmail, emailMessage.RecipientName));

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync(smtpOptions.Value.Host, smtpOptions.Value.Port, cancellationToken: cancellationToken);
        await smtp.AuthenticateAsync(smtpOptions.Value.Username, smtpOptions.Value.Password, cancellationToken);
        await smtp.SendAsync((MimeMessage)mail, cancellationToken);

        emailMessage.Sent = true;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
