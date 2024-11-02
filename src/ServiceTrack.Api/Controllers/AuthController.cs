using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Users;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : Controller
{
    /// <summary>
    /// Logs in the user with the given information
    /// </summary>
    /// <param name="loginDto">Email and password to login user</param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/Login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var generateClaimsPrincipalForUserCommand = new GenerateClaimsPrincipalForUserCommand(loginDto);
        var userPrincipal = await mediator.Send(generateClaimsPrincipalForUserCommand);

        await HttpContext.SignInAsync(userPrincipal);

        return NoContent();
    }

    /// <summary>
    /// Registers a new user with the given information. The user is not logged in after registration
    /// </summary>
    /// <param name="registerUserDto">Information needed to create new user with specified password</param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/Register")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserDto registerUserDto
    )
    {
        var registerNewUserCommand = new RegisterNewUserCommand(registerUserDto);
        await mediator.Send(registerNewUserCommand);

        var addPasswordToRegisteredUserCommand = new AddPasswordToRegisteredUserCommand(new AddPasswordToRegisteredUserDto
        {
            Email = registerUserDto.Email,
            Password = registerUserDto.Password
        });
        await mediator.Send(addPasswordToRegisteredUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Generates a token for the user to confirm their email
    /// </summary>
    /// <param name="email">Users email that specifies which user should get the token</param>
    /// <returns></returns>
    [HttpGet("api/v1/Auth/GenerateEmailConfirmToken")]
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
    [HttpPost("api/v1/Auth/ValidateToken")]
    public async Task<ActionResult> ValidateToken(
        [FromBody] EmailConfirmTokenForUserDto confirmTokenForUserDto
    )
    {
        var validateEmailConfirmationTokenForUserCommand = new ValidateEmailConfirmationTokenCommand(confirmTokenForUserDto);
        await mediator.Send(validateEmailConfirmationTokenForUserCommand);

        return NoContent();
    }

    //TODO: Move to a different controller
    /// <summary>
    /// Creates a user with the given information
    /// </summary>
    /// <param name="createNewUserDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/CreateUser")]
    public async Task<ActionResult> CreateUser(
        [FromBody] CreateNewUserDto createNewUserDto
    )
    {
        var createNewUserCommand = new CreateNewUserCommand(createNewUserDto);
        await mediator.Send(createNewUserCommand);

        return NoContent();
    }
}
