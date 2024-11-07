using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Application.Contracts.Tenants.Queries.Dto;
using TenantDetailDto = ServiceTrack.Application.Contracts.Tenants.Commands.Dto.TenantDetailDto;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class TenantController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a tenant with the given information
    /// </summary>
    /// <param name="newTenantDto">Information that user provides</param>
    /// <returns></returns>
    [HttpPost("api/v1/tenants")]
    public async Task<ActionResult<Guid>> CreateTenant(
        [FromBody] NewTenantDto newTenantDto
    )
    {
        var createNewTenantCommand = new CreateNewTenantCommand(newTenantDto);
        var tenantId = await mediator.Send(createNewTenantCommand);

        return Ok(tenantId);
    }

    /// <summary>
    /// Updates a tenant with the given information
    /// </summary>
    /// <param name="tenantDetailDto">Information that user provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/tenants")]
    public async Task<ActionResult<Guid>> UpdateTenant(
        [FromBody] TenantDetailDto tenantDetailDto
    )
    {
        var updateTenantCommand = new UpdateTenantCommand(tenantDetailDto);
        await mediator.Send(updateTenantCommand);

        return NoContent();
    }

    /// <summary>
    /// Gets the tenant with the given id
    /// </summary>
    /// <param name="id">Id of tenant</param>
    /// <returns></returns>
    [HttpGet("api/v1/tenants/{id}")]
    public async Task<ActionResult<TenantDetailDto>> GetTenant(
        [FromRoute] Guid id
    )
    {
        var getTenantByIdQuery = new GetEntityByIdQuery<TenantDetailDto>(id);
        var tenant = await mediator.Send(getTenantByIdQuery);

        return Ok(tenant);
    }

    /// <summary>
    /// Gets the list of tenants
    /// </summary>
    /// <returns></returns>
    [HttpGet("api/v1/tenants")]
    public async Task<ActionResult<List<TenantListDto>>> GetTenantList()
    {
        var getTenantListQuery = new GetListOfEntityQuery<TenantListDto>();
        var tenants = await mediator.Send(getTenantListQuery);

        return Ok(tenants);
    }
}
