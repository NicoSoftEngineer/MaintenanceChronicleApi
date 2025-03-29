using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command to generate email confirmation email for user.
/// </summary>
/// <param name="email">User for whom the email is generated</param>
/// <param name="token">Generated email confirmation toke</param>
/// <returns>Generated email with EmailConfirmation template</returns>
public class GenerateEmailConfirmationEmailForUserCommand(string email, string token) : IRequest<NewEmailMessageDto>
{
    public string Email { get; set; } = email;
    public string Token { get; set; } = token;
}
