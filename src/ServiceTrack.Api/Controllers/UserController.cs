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
        var createNewUserCommand = new CreateNewUserCommand(createNewUserDto, HttpContext.User.GetUserId(), HttpContext.User.GetTenantId());
        var userId = await mediator.Send(createNewUserCommand);

        var addRolesToUserCommand = new AddRolesToUserCommand(
            new UserRolesDto
            {
                UserId = userId,
                RoleIds = createNewUserDto.Roles
            },
            HttpContext.User.GetUserId(),
            HttpContext.User.GetTenantId()
        );
        await mediator.Send(addRolesToUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Creates a user with the given information
    /// </summary>
    /// <param name="userDetailDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/users")]
    public async Task<ActionResult> UpdateUser(
        [FromBody] UserDetailDto userDetailDto
    )
    {
        var createNewUserCommand = new UpdateUserCommand(userDetailDto, HttpContext.User.GetUserId());
        await mediator.Send(createNewUserCommand);

        var addRolesToUserCommand = new AddRolesToUserCommand(
            new UserRolesDto
            {
                UserId = userDetailDto.Id,
                RoleIds = userDetailDto.Roles
            },
            HttpContext.User.GetUserId(),
            HttpContext.User.GetTenantId()
        );
        await mediator.Send(addRolesToUserCommand);

        return NoContent();
    }
}
