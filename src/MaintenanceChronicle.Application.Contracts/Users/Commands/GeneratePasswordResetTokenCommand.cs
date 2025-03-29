using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command to generate password reset token.
/// </summary>
/// <param name="Email">User for whom the token is generated</param>
/// <returns>Unmodified password token</returns>
public record GeneratePasswordResetTokenCommand(string Email) : IRequest<string>;
