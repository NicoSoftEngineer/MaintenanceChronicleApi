using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : Controller
{
    [HttpPost("api/v1/Auth/Login")]
    public async Task<ActionResult> Login([FromBody] LoginDto model)
    {
        var generateClaimsPrincipalForUserCommand = new GenerateClaimsPrincipalForUserCommand(model);
        var userPrincipal = await mediator.Send(generateClaimsPrincipalForUserCommand);

        await HttpContext.SignInAsync(userPrincipal);

        return NoContent();
    }

    [HttpPost("api/v1/Auth/Register")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserDto model
    )
    {
        var registerNewUserCommand = new RegisterNewUserCommand(model);
        await mediator.Send(registerNewUserCommand);

        var addPasswordToRegisteredUserCommand = new AddPasswordToRegisteredUserCommand(new AddPasswordToRegisteredUserDto
        {
            Email = model.Email,
            Password = model.Password
        });
        await mediator.Send(addPasswordToRegisteredUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Generates a token for the user to confirm their email
    /// </summary>
    /// <param name="email"></param>
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
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/ValidateToken")]
    public async Task<ActionResult> ValidateToken(
        [FromBody] EmailConfirmTokenForUserDto model
    )
    {
        var validateEmailConfirmationTokenForUserCommand = new ValidateEmailConfirmationTokenCommand(model);
        await mediator.Send(validateEmailConfirmationTokenForUserCommand);

        return NoContent();
    }
}

