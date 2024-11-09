using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public record UpdateUserCommand(UserDetailDto UserDetailDto, string UserId) : IRequest;
