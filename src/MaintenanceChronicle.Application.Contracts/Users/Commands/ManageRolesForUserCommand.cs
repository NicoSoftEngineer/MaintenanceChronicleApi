using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record ManageRolesForUserCommand(
    UserRolesDto UserRoles,
    string UserId,
    string TenantId) : IRequest;
