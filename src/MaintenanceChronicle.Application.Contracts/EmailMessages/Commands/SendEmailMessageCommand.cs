using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;

/// <summary>
/// Sends the email, and sets the property Sent of an email message to be true
/// </summary>
/// <param name="MessageId"></param>
public record SendEmailMessageCommand(Guid MessageId) : IRequest;
