using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public record UpdateUserCommand(UpdateUserDetailDto UpdateUserDetailDto, string UserId) : IRequest;
