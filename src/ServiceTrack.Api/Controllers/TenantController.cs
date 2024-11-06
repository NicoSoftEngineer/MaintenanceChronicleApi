using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;
using ServiceTrack.Application.Contracts.Tenants.Queries;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Utils.Queries;

namespace ServiceTrack.Api.Controllers;

[ApiController]
public class TenantController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a tenant with the given information
    /// </summary>
    /// <param name="newTenantDto">Information that user provides</param>
    /// <returns></returns>
    [HttpPost("api/v1/Tenant/new")]
    public async Task<ActionResult<Guid>> CreateTenant(
        [FromBody] NewTenantDto newTenantDto
    )
    {
        var createNewTenantCommand = new CreateNewTenantCommand(newTenantDto);
        var tenantId = await mediator.Send(createNewTenantCommand);

        return Ok(tenantId);
    }

    /// <summary>
    /// Gets the tenant with the given id
    /// </summary>
    /// <param name="id">Id of tenant</param>
    /// <returns></returns>
    [HttpGet("api/v1/Tenant/{id}")]
    public async Task<ActionResult<TenantDto>> GetTenant(
        [FromRoute] Guid id
    )
    {
        var getTenantByIdQuery = new GetEntityByIdQuery<TenantDto>(id);
        var tenant = await mediator.Send(getTenantByIdQuery);

        return Ok(tenant);
    }
}
