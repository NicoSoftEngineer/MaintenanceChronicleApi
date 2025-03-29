using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Generates password reset email for defined user
/// </summary>
/// <param name="Email">Users email, that wants its password reset</param>
/// <param name="Token">Generated user token</param>
/// <returns>New email message with Reset password email</returns>
public record GeneratePasswordResetEmailForUserCommand(string Email, string Token) : IRequest<NewEmailMessageDto>;
