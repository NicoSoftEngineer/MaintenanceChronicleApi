using MaintenanceChronicle.Data.Entities.Business;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;

public class NewEmailMessageDto
{
    public required string RecipientEmail { get; set; }
    public string? RecipientName { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public Instant? SendAt { get; set; } = null;
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
}

public static class NewEmailMessageExtension
{
    public static EmailMessage ToEntity(this NewEmailMessageDto dto, Instant createdAt) => new EmailMessage
    {
        Body = dto.Body,
        FromEmail = dto.FromEmail,
        FromName = dto.FromName,
        RecipientEmail = dto.RecipientEmail,
        RecipientName = dto.RecipientName,
        Subject = dto.Subject,
        Sent = false,
        SendAt = dto.SendAt ?? createdAt,
    };
}
