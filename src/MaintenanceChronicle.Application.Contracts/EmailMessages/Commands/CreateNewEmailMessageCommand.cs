using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;

public record CreateNewEmailMessageCommand(NewEmailMessageDto NewEmailMessage) : IRequest;
