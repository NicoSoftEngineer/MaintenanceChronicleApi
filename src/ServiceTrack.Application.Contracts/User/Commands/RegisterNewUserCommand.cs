using MediatR;
using ServiceTrack.Application.Contracts.User.Commands.Dto;

namespace ServiceTrack.Application.Contracts.User.Commands;

public record RegisterNewUserCommand(RegisterUserDto NewUserDto) : IRequest<RegisterNewUserCommandResult>;

public enum RegisterNewUserCommandResult
{
    Success,
    EmailAlreadyExists
}
