using MaintenanceChronicle.Data.Entities.Business;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NodaTime;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;

public class EmailMessageInListDto
{
    public Guid Id { get; set; }
    public IDictionary<string, string?> Recipients { get; set; } = new Dictionary<string, string?>();

    public required string Subject { get; set; }
    public required string Body { get; set; }
    public bool Sent { get; set; }
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }
}
/// <summary>
/// Extension methods for <see cref="EmailMessageInListDto"/> entity.
/// </summary>
public static class EmailMessageInListExtension
{
    /// <summary>
    /// Converts <see cref="EmailMessage"/> to <see cref="EmailMessageInListDto"/>.
    /// </summary>
    /// <param name="entity"><see cref="EmailMessage"/> to convert</param>
    /// <returns>Converted entity to <see cref="EmailMessageInListDto"/></returns>
    public static EmailMessageInListDto ToListDto(this EmailMessage entity) => new EmailMessageInListDto
    {
        Id = entity.Id,
        Body = entity.Body,
        FromEmail = entity.FromEmail,
        FromName = entity.FromName,
        Recipients = entity.Recipients,
        Sent = entity.Sent,
        Subject = entity.Subject,
    };
}
