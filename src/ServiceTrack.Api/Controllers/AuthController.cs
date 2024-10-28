using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.User.Commands;
using ServiceTrack.Application.Contracts.User.Commands.Dto;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : Controller
{
    [HttpPost("api/v1/Auth/Login")]
    public async Task<ActionResult> Login(/*[FromBody] LoginDto model*/)
    {
        //var userPrincipal = await authService.GetClaimsPrincipalForUser(model);

        //await HttpContext.SignInAsync(userPrincipal);

        return NoContent();
    }

    [HttpPost("api/v1/Auth/Register")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserDto model
    )
    {
        var command = new RegisterNewUserCommand(model);
        var result = await mediator.Send(command);

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

