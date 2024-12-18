using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public class GenerateEmailConfirmationTokenForUserCommand(string email) : IRequest<string>
{
    public string Email { get; set; } = email;
}
