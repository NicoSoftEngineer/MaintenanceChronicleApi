using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;
/// <summary>
/// Command to generate email confirmation token.
/// </summary>
/// <param name="Email">User for whom the token is generated</param>
/// <returns>Unmodified generated email confirmation token</returns>
public record GenerateEmailConfirmTokenCommand(string Email) : IRequest<string>;
