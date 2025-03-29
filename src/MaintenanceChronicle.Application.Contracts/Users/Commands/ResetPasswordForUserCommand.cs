using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

/// <summary>
/// Command to reset password for a user.
/// </summary>
/// <param name="UserResetPasswordDto">Dto with reset token, user email and the new password</param>
public record ResetPasswordForUserCommand(UserResetPasswordDto UserResetPasswordDto) : IRequest;
