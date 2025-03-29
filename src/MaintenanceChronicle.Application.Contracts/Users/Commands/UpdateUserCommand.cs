using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command for updating user.
/// </summary>
/// <param name="Patch">JsonPatchDocument with instructions on what to replace</param>
/// <param name="ModifiedUserId">ID of modified user</param>
/// <param name="UserId">ID of user who requested the modification</param>
/// <returns>Updated user dto</returns>
public record UpdateUserCommand(JsonPatchDocument<UpdateUserDetailDto> Patch, Guid ModifiedUserId, string UserId) : IRequest<UpdateUserDetailDto>;
