using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public class GenerateEmailConfirmationEmailForUserCommand(string email) : IRequest<NewEmailMessageDto>
{
    public string Email { get; set; } = email;
}
