using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command to validate email confirmation token.
/// </summary>
/// <param name="emailConfirmTokenForUserDto">dto with token and user email</param>
public class ValidateEmailConfirmationTokenCommand(EmailConfirmTokenForUserDto emailConfirmTokenForUserDto) : IRequest
{
    public EmailConfirmTokenForUserDto EmailConfirmTokenForUserDto { get; set; } = emailConfirmTokenForUserDto;
}
