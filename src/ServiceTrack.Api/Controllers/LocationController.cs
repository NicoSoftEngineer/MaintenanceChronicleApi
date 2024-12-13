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
}
