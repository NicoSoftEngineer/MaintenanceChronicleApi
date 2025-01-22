using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;

public record SendEmailMessageCommand(EmailMessageInListDto EmailMessage) : IRequest;
