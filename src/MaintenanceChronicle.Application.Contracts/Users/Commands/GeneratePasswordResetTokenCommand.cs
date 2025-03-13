using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record GeneratePasswordResetTokenCommand(string Email) : IRequest<string>;
