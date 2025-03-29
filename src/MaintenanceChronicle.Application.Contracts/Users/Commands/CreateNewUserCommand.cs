using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command to create a new user.
/// </summary>
/// <param name="NewUserDto">Dto with info about the new user</param>
/// <param name="UserId">ID of requesting user</param>
/// <param name="TenantId">ID of the responsible tenant</param>
public record CreateNewUserCommand(CreateNewUserDto NewUserDto, string UserId, string TenantId) : IRequest<Guid>;
