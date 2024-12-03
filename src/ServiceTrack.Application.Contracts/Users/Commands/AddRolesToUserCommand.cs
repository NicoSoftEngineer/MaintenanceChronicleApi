using MediatR;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Users.Commands;

public record AddRolesToUserCommand(UserRolesDto UserRoles, string UserId, string TenantId) : IRequest;
