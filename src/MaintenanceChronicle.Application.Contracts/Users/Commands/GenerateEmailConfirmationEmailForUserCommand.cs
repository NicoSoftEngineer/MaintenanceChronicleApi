using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public class GenerateEmailConfirmationEmailForUserCommand(string email, string token) : IRequest<NewEmailMessageDto>
{
    public string Email { get; set; } = email;
    public string Token { get; set; } = token;
}
