using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public record CreateNewUserCommand(CreateNewUserDto NewUserDto, string UserId, string TenantId) : IRequest<Guid>;
