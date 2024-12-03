using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Utilities.Helpers;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Utilities.Constants;

namespace ServiceTrack.Api.Controllers;

//Makes endpoints accessible only for users with Admin or GlobalAdmin roles
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin}")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a user with the given information
    /// </summary>
    /// <param name="createNewUserDto">Information that admin provides</param>
    /// <returns>New user id</returns>
    [HttpPost("api/v1/users")]
    public async Task<ActionResult<Guid>> CreateUser(
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

        return Ok(userId);
    }

    /// <summary>
    /// Updates a user with the given information
    /// </summary>
    /// <param name="userDetailDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/users")]
    public async Task<ActionResult> UpdateUser(
        [FromBody] UpdateUserDetailDto userDetailDto
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

    /// <summary>
    /// Gets a list of users
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet("api/v1/users")]
    public async Task<ActionResult> GetUserList()
    {
        var usersQuery = new GetListOfEntityQuery<UserListDto>();
        var users = await mediator.Send(usersQuery);

        return Ok(users);
    }

    /// <summary>
    /// Gets a specific user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>user</returns>
    [HttpGet("api/v1/users/{id:guid}")]
    public async Task<ActionResult> GetUser(
        [FromRoute] Guid id
    )
    {
        var userQuery = new GetEntityByIdQuery<UserDetailDto>(id);
        var user = await mediator.Send(userQuery);

        return Ok(user);
    }
}
