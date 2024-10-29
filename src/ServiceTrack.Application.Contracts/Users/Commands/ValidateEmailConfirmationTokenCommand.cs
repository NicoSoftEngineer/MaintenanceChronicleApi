using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public class ValidateEmailConfirmationTokenCommand(EmailConfirmTokenForUserDto emailConfirmTokenForUserDto) : IRequest
{
    public EmailConfirmTokenForUserDto EmailConfirmTokenForUserDto { get; set; } = emailConfirmTokenForUserDto;
}
