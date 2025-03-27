using MaintenanceChronicle.Data.Entities.Business;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Text;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;

public class NewEmailMessageDto
{
    public Dictionary<string, string?> Recipients { get; set; } = new Dictionary<string, string?>();
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public string? SendAt { get; set; } = null;
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
        Recipients = dto.Recipients,
        Subject = dto.Subject,
        Sent = false,
        CreatedAt = createdAt,
        SendAt = !string.IsNullOrEmpty(dto.SendAt) ? InstantPattern.General.Parse(dto.SendAt!.Split("T")[0] + "T12:00:00Z").Value : createdAt,
    };
}
