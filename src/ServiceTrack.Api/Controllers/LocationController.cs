using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using ServiceTrack.Application.Contracts.Locations.Commands;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;
using ServiceTrack.Utilities.Constants;
using Microsoft.AspNetCore.JsonPatch;

namespace ServiceTrack.Api.Controllers;

//Makes endpoints accessible only for users with Admin or GlobalAdmin roles
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin}")]
[ApiController]
public class LocationController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates customer with the given information
    /// </summary>
    /// <param name="newLocationDto">Information that admin provides</param>
    /// <returns>New customer id</returns>
    [HttpPost("api/v1/locations")]
    public async Task<ActionResult<Guid>> CreateLocation(
        [FromBody] NewLocationDto newLocationDto
    )
    {
        var createNewLocationCommand = new CreateNewLocationCommand(
            newLocationDto,
            User.GetUserId(),
            User.GetTenantId()
        );
        var locationId = await mediator.Send(createNewLocationCommand);

        return Ok(locationId);
    }

    /// <summary>
    /// Updates customer with the given information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/locations/{id:guid}")]
    public async Task<ActionResult<ManageLocationDetailDto>> UpdateLocation(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<ManageLocationDetailDto> patch
    )
    {
        var updateLocationCommand = new UpdateLocationCommand(
            patch,
            id,
            User.GetUserId()
        );
        var updatedLocation = await mediator.Send(updateLocationCommand);

        return Ok(updatedLocation);
    }
}
