using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;
using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Roles.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Commands;
using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Utilities.Constants;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceChronicle.Api.Controllers;

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

        var addRolesToUserCommand = new ManageRolesForUserCommand(
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
    /// <param name="id">Edited user id</param>
    /// <param name="userDetailDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/users/{id:guid}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute]Guid id,
        [FromBody] JsonPatchDocument<UpdateUserDetailDto> userDetailDto
    )
    {
        var createNewUserCommand = new UpdateUserCommand(userDetailDto, id, HttpContext.User.GetUserId());
        await mediator.Send(createNewUserCommand);

        return NoContent();
    }

    /// <summary>
    /// Updates user roles
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="roles">New roles</param>
    /// <returns></returns>
    [HttpPost("api/v1/users/{id:guid}/roles")]
    public async Task<ActionResult> ManageUserRoles(
        [FromRoute] Guid id,
        [FromBody] RoleDetailDto[] roles
    )
    {
        var userDetailDto = new UserRolesDto
        {
            UserId = id,
            RoleIds = roles.Select(r => r.Id).ToArray()
        };
        var manageRoles = new ManageRolesForUserCommand(userDetailDto, HttpContext.User.GetUserId(), User.GetTenantId());
        await mediator.Send(manageRoles);

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

    /// <summary>
    /// Gets the list of locations in which the specified user is contact
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>List of locations</returns>
    [HttpGet("api/v1/users/{id:guid}/locations")]
    public async Task<ActionResult<List<LocationInListForContactDto>>> GetUsersLocations([FromRoute] Guid id)
    {
        var query = new GetListOfLocationsForContactUserQuery(id);
        var locations = await mediator.Send(query);

        return Ok(locations);
    }

    /// <summary>
    /// Gets all the available roles for user
    /// </summary>
    /// <returns>List of roles</returns>
    [HttpGet("api/v1/users/roles")]
    public async Task<ActionResult<List<RoleDetailDto>>> GetRoles()
    {
        var query = new GetListOfEntityQuery<RoleDetailDto>();
        var roles = await mediator.Send(query);

        return Ok(roles);
    }
}
