using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Generate email, that invites user to the app and prompts him to create password
/// </summary>
/// <param name="Email">Users email</param>
/// <param name="PasswordToken">Token to allow user to change his password</param>
/// <param name="ConfToken">Email confirmation email</param>
/// <returns>New email message with Invitation and password creation email</returns>
public record GeneratePasswordCreateEmailForUserCommand(string Email, string PasswordToken, string ConfToken) : IRequest<NewEmailMessageDto>;
