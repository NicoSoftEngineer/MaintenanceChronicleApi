using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command for managing roles for user.
/// </summary>
/// <param name="UserRoles">Dto with user ID and roles the user should have</param>
/// <param name="UserId">User id of who made the request</param>
/// <param name="TenantId">Current tenant id</param>
public record ManageRolesForUserCommand(
    UserRolesDto UserRoles,
    string UserId,
    string TenantId) : IRequest;
