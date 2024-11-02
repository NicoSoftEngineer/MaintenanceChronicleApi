using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Tenants.Commands;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;

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
}
