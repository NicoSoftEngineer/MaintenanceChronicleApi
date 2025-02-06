namespace MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;

public class UserResetPasswordDto
{
    public required string Email { get; set; }
    public required string ResetToken { get; set; }
    public required string NewPassword { get; set; }
}
