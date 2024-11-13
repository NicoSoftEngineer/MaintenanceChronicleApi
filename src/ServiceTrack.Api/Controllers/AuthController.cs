using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;
using ServiceTrack.Application.Contracts.Users;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Queries;
using ServiceTrack.Utilities.Helpers;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : Controller
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

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipalWithTenantClaim);

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

        return Ok(result);
    }

    /// <summary>
    /// Generates a token for the user to confirm their email
    /// </summary>
    /// <param name="email">Users email that specifies which user should get the token</param>
    /// <returns></returns>
    [HttpGet("api/v1/auth/generateEmailConfirmToken")]
    public async Task<ActionResult<string>> GenerateToken([FromQuery] string email)
    {
        var generateEmailConfirmationTokenForUserCommand = new GenerateEmailConfirmationTokenForUserCommand(email);
        var token = await mediator.Send(generateEmailConfirmationTokenForUserCommand);

        return Ok(token);
    }

    /// <summary>
    /// Validates the token that the user has received in their email
    /// </summary>
    /// <param name="confirmTokenForUserDto">Email and the given token for email confirmation</param>
    /// <returns></returns>
    [HttpPost("api/v1/auth/validateToken")]
    public async Task<ActionResult> ValidateToken(
        [FromBody] EmailConfirmTokenForUserDto confirmTokenForUserDto
    )
    {
        var validateEmailConfirmationTokenForUserCommand = new ValidateEmailConfirmationTokenCommand(confirmTokenForUserDto);
        await mediator.Send(validateEmailConfirmationTokenForUserCommand);

        return NoContent();
    }
}
