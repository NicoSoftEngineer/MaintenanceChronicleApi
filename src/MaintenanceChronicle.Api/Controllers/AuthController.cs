using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Queries;
using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.UserTenant.Commands;
using MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceChronicle.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Logs in the user with the given information
    /// </summary>
    /// <param name="loginDto">Email and password to login user</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var generateClaimsPrincipalForUserCommand = new GenerateClaimsPrincipalForUserCommand(loginDto);
        var userPrincipal = await mediator.Send(generateClaimsPrincipalForUserCommand);

        var getTenantIdForUserCommand = new GetTenantIdFromUserCommand(loginDto.Email);
        var tenantId = await mediator.Send(getTenantIdForUserCommand);

        var userTenantClaimDto = new UserTenantClaimDto
        {
            Email = loginDto.Email,
            TenantId = tenantId
        };

        var addTenantClaimToUserPrincipalCommand = new AddTenantClaimToUserPrincipalCommand(userTenantClaimDto, userPrincipal);
        var userPrincipalWithTenantClaim = await mediator.Send(addTenantClaimToUserPrincipalCommand);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30), // Set custom expiration time
            IsPersistent = true
        };

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipalWithTenantClaim, authProperties);

        return NoContent();
    }

    [Authorize]
    [HttpGet("api/v1/auth/logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        Response.Cookies.Delete(".AspNetCore.Identity.Application");
        return NoContent();
    }

    /// <summary>
    /// Registers a new user with the given information. The user is not logged in after registration
    /// </summary>
    /// <param name="registerUserDto">Information needed to create new user with specified password</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/register")]
    public async Task<ActionResult<Guid>> Register(
        [FromBody] RegisterUserDto registerUserDto
    )
    {
        var registerNewUserCommand = new RegisterNewUserCommand(registerUserDto);
        var result = await mediator.Send(registerNewUserCommand);

        var addPasswordToRegisteredUserCommand = new AddPasswordToRegisteredUserCommand(new AddPasswordToRegisteredUserDto
        {
            Email = registerUserDto.Email,
            Password = registerUserDto.Password
        });
        await mediator.Send(addPasswordToRegisteredUserCommand);

        var getRoleByNameCommand = new GetEntityByNameQuery<RoleDetailDto>(RoleTypes.Admin);
        var adminRole = await mediator.Send(getRoleByNameCommand);

        var addRolesToUserCommand = new AddRolesToUserCommand(
        new UserRolesDto
            {
                UserId = result,
                RoleIds = new Guid[1] { adminRole.Id }
            },
            result.ToString(),
            registerUserDto.TenantId.ToString()
        );
        await mediator.Send(addRolesToUserCommand);

        return Ok(result);
    }

    /// <summary>
    /// Registers a new user and tenant with the given information. The user is not logged in after registration
    /// </summary>
    /// <param name="registerUserTenantDto">Information needed to create new user with specified password and tenant</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/register-user-tenant")]
    public async Task<ActionResult<Guid>> RegisterUserTenant(
        [FromBody] RegisterUserTenantDto registerUserTenantDto
    )
    {
        var registerNewUserCommand = new RegisterUserAndTenantCommand(registerUserTenantDto);
        var result = await mediator.Send(registerNewUserCommand);

        var getRoleByNameCommand = new GetEntityByNameQuery<RoleDetailDto>(RoleTypes.Admin);
        var adminRole = await mediator.Send(getRoleByNameCommand);

        var addRolesToUserCommand = new AddRolesToUserCommand(
            new UserRolesDto
            {
                UserId = result.UserId,
                RoleIds = new Guid[1] { adminRole.Id }
            },
            result.UserId.ToString(),
            result.TenantId.ToString()
        );
        await mediator.Send(addRolesToUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Generates and sends a token for the user to confirm their email
    /// </summary>
    /// <param name="email">Users email that specifies which user should get the email</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/send-email-confirm-email")]
    public async Task<ActionResult> GenerateEmailConfirmationEmail([FromQuery] string email)
    {
        var generateEmailConfirmationTokenForUserCommand = new GenerateEmailConfirmationEmailForUserCommand(email);
        var emailToBeSent = await mediator.Send(generateEmailConfirmationTokenForUserCommand);

        var createEmailToBeSendCommand = new CreateNewEmailMessageCommand(emailToBeSent);
        await mediator.Send(createEmailToBeSendCommand);

        return Ok();
    }

    /// <summary>
    /// Validates the token that the user has received in their email
    /// </summary>
    /// <param name="confirmTokenForUserDto">Email and the given token for email confirmation</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/validate-token")]
    public async Task<ActionResult> ValidateToken(
        [FromQuery] EmailConfirmTokenForUserDto confirmTokenForUserDto
    )
    {
        var validateEmailConfirmationTokenForUserCommand = new ValidateEmailConfirmationTokenCommand(confirmTokenForUserDto);
        await mediator.Send(validateEmailConfirmationTokenForUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Generates and sends a token for the user to reset their password
    /// </summary>
    /// <param name="email">Users email that specifies which user should get the email</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/send-password-reset")]
    public async Task<ActionResult> GeneratePasswordResetEmail([FromQuery] string email)
    {
        var generatePasswordResetEmailForUserCommand = new GeneratePasswordResetEmailForUserCommand(email);
        var emailToBeSent = await mediator.Send(generatePasswordResetEmailForUserCommand);

        var createEmailToBeSendCommand = new CreateNewEmailMessageCommand(emailToBeSent);
        await mediator.Send(createEmailToBeSendCommand);

        return Ok();
    }

    /// <summary>
    /// Resets the password for user, when the reset token is valid for that user
    /// </summary>
    /// <param name="userResetPasswordDto"></param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPasswordDto)
    {
        var command = new ResetPasswordForUserCommand(userResetPasswordDto);
        await mediator.Send(command);

        return NoContent();
    }

    [HttpGet("api/v1/auth/current-user-info")]
    public async Task<ActionResult<LoggedInUserInfoDto>> GetCurrentUserInfo()
    {
        var query = new GetCurrentUserInfoQuery(User.GetUserId());
        var info = await mediator.Send(query);

        return info;
    }
}
