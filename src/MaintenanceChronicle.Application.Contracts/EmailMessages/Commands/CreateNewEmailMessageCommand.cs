using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
/// <summary>
/// Command to create new email message
/// </summary>
/// <param name="NewEmailMessage"><see cref="NewEmailMessageDto"/> with info</param>
/// <returns>Guid of the created email</returns>
public record CreateNewEmailMessageCommand(NewEmailMessageDto NewEmailMessage) : IRequest<Guid>;
