using MediatR;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public class GenerateEmailConfirmationTokenForUserCommand(string email) : IRequest<string>
{
    public string Email { get; set; } = email;
}
