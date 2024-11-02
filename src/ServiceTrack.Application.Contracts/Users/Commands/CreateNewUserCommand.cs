using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

//TODO: Create command should return the new user id
public record CreateNewUserCommand(CreateNewUserDto NewUserDto) : IRequest;
