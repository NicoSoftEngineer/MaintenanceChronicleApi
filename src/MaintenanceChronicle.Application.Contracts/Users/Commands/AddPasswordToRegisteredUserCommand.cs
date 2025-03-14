using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record AddPasswordToRegisteredUserCommand(AddPasswordToUserDto UserPasswordDto) : IRequest;
