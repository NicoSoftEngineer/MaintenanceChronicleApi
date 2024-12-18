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
using ServiceTrack.Application.Contracts.LocationContactUsers.Queries;
using ServiceTrack.Application.Contracts.LocationContactUsers.Queries.Dto;
using ServiceTrack.Application.Contracts.Locations.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Commands;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Data.Entities.Business;

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

    /// <summary>
    /// Gets the list of locations
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/v1/locations")]
    public async Task<ActionResult<List<LocationInListDto>>> GetLocationList()
    {
        var getLocationsQuery = new GetListOfEntityQuery<LocationInListDto>();
        var locationList = await mediator.Send(getLocationsQuery);

        return Ok(locationList);
    }

    /// <summary>
    /// Get location by id
    /// </summary>
    /// <param name="id">Id that user provides</param>
    /// <returns></returns>
    [HttpGet("/api/v1/location/{id:guid}")]
    public async Task<ActionResult<List<LocationDetailDto>>> GetLocation(Guid id)
    {
        var getLocationQuery = new GetEntityByIdQuery<LocationDetailDto>(id);
        var location = await mediator.Send(getLocationQuery);

        return Ok(location);
    }

    /// <summary>
    /// Delete the specified location
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("/api/v1/location/{id:guid}")]
    public async Task<ActionResult> DeleteLocation(Guid id)
    {
        var deleteCommand = new DeleteEntityByIdCommand<Location>(id, User.GetUserId());
        await mediator.Send(deleteCommand);

        return NoContent();
    }

    /// <summary>
    /// Adds contact to Location
    /// </summary>
    /// <param name="contactsInLocationDto"></param>
    /// <returns></returns>
    [HttpPost("/api/v1/location/contacts")]
    public async Task<ActionResult> AssignContactsToLocation(ContactsInLocationDto contactsInLocationDto)
    {
        var assignCommand =
            new AssignContactsToLocationCommand(contactsInLocationDto, User.GetUserId(), User.GetTenantId());
        await mediator.Send(assignCommand);

        return NoContent();
    }

    /// <summary>
    /// Gets contacts for specified location
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/api/v1/location/{id:guid}/contacts")]
    public async Task<ActionResult<List<LocationContactInListDto>>> GetContactsForLocation([FromRoute] Guid id)
    {
        var getContactsQuery = new GetListOfContactsForLocationQuery(id);
        var contacts = await mediator.Send(getContactsQuery);

        return Ok(contacts);
    }
}
