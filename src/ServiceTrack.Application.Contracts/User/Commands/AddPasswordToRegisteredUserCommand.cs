using MediatR;
using ServiceTrack.Application.Contracts.User.Commands.Dto;

namespace ServiceTrack.Application.Contracts.User.Commands;

public record AddPasswordToRegisteredUserCommand(AddPasswordToRegisteredUserDto UserPasswordDto) : IRequest<AddPasswordToRegisteredUserCommandResult>;

public enum AddPasswordToRegisteredUserCommandResult
{
    Success,
    UserNotFound,
    PasswordDoesNotMeetRequirements
}
