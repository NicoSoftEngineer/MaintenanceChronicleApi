using MaintenanceChronicle.Data.Entities.Business;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;

public class EmailMessageInListDto
{
    public Guid Id { get; set; }
    public required string RecipientEmail { get; set; }
    public string? RecipientName { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public bool Sent { get; set; }
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }
}

public static class EmailMessageInListExtension
{
    public static EmailMessageInListDto ToListDto(this EmailMessage entity) => new EmailMessageInListDto
    {
        Id = entity.Id,
        Body = entity.Body,
        FromEmail = entity.FromEmail,
        FromName = entity.FromName,
        RecipientEmail = entity.RecipientEmail,
        RecipientName = entity.RecipientName,
        Sent = entity.Sent,
        Subject = entity.Subject,
    };
}
