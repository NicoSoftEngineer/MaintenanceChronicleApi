using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record AddRolesToUserCommand(UserRolesDto UserRoles, string UserId, string TenantId) : IRequest;
