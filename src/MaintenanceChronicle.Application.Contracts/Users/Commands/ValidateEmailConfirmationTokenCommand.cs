using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public class ValidateEmailConfirmationTokenCommand(EmailConfirmTokenForUserDto emailConfirmTokenForUserDto) : IRequest
{
    public EmailConfirmTokenForUserDto EmailConfirmTokenForUserDto { get; set; } = emailConfirmTokenForUserDto;
}
