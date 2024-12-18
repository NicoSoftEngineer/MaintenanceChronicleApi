using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record RegisterNewUserCommand(RegisterUserDto NewUserDto) : IRequest<Guid>;

