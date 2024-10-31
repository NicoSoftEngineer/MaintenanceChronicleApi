using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users;

public record CreateNewUserCommand(CreateNewUserDto NewUserDto) : IRequest;
