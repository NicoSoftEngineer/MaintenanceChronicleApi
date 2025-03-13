using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record GenerateEmailConfirmTokenCommand(string Email) : IRequest<string>;
