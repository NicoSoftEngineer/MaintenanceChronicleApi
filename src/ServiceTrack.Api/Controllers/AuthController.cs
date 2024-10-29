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
    /// unescape token before sending
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("api/v1/Auth/ValidateToken")]
    public async Task<ActionResult> ValidateToken(
        //[FromBody] TokenDto model
    )
    {
        //await authService.ValidateToken(model);

        return NoContent();
    }
}

