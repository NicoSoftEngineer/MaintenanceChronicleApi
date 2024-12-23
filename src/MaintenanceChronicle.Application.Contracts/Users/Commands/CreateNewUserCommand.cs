using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record CreateNewUserCommand(CreateNewUserDto NewUserDto, string UserId, string TenantId) : IRequest<Guid>;