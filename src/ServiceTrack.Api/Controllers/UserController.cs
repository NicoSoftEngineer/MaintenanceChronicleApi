using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Utilities.Helpers;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class UserController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a user with the given information
    /// </summary>
    /// <param name="createNewUserDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPost("api/v1/users")]
    public async Task<ActionResult> CreateUser(
        [FromBody] CreateNewUserDto createNewUserDto
    )
    {
        var createNewUserCommand = new CreateNewUserCommand(createNewUserDto, HttpContext.User.GetUserId());
        await mediator.Send(createNewUserCommand);

        return NoContent();
    }
}
