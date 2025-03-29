using MaintenanceChronicle.Application.Contracts.Tenants.Commands;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MaintenanceChronicle.Application.Contracts.Tenants.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Utilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TenantDetailDto = MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto.TenantDetailDto;

namespace MaintenanceChronicle.Api.Controllers;

[ApiController]
public class TenantController(IMediator mediator) : ControllerBase
{
    //TODO: Remove unusable endpoints

    /// <summary>
    /// Updates a tenant with the given information
    /// </summary>
    /// <param name="tenantDetailDto">Information that user provides</param>
    /// <param name="id">ID of tenant to be updated</param>
    /// <returns></returns>
    [HttpPatch("api/v1/tenants/{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateTenant([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<TenantDetailDto> tenantDetailDto
    )
    {
        var updateTenantCommand = new UpdateTenantCommand(id, tenantDetailDto, HttpContext.User.GetUserId());
        var result = await mediator.Send(updateTenantCommand);

        return Ok(result);
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
}
