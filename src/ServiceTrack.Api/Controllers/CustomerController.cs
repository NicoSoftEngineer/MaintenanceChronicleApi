using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Utilities.Helpers;
using ServiceTrack.Utilities.Constants;

namespace ServiceTrack.Api.Controllers;

//Makes endpoints accessible only for users with Admin or GlobalAdmin roles
[Authorize(Roles = $"{RoleTypes.Admin},{RoleTypes.GlobalAdmin}")]
[ApiController]
public class CustomerController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates customer with the given information
    /// </summary>
    /// <param name="newCustomerDto">Information that admin provides</param>
    /// <returns>New customer id</returns>
    [HttpPost("api/v1/customers")]
    public async Task<ActionResult<Guid>> CreateUser(
        [FromBody] NewCustomerDto newCustomerDto
    )
    {
        var createNewCustomerDto = new CreateNewCustomerCommand(
            newCustomerDto,
            User.GetUserId(),
            User.GetTenantId()
        );
        var customerId = await mediator.Send(createNewCustomerDto);

        return Ok(customerId);
    }

    /// <summary>
    /// Updates customer with the given information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customerDetailDto">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/customers/{id:guid}")]
    public async Task<ActionResult<CustomerDetailDto>> UpdateCustomer(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<CustomerDetailDto> patch
    )
    {
        var updateCustomerCommand = new UpdateCustomerCommand(
            patch,
            id,
            User.GetUserId()
        );
        var updatedCustomer = await mediator.Send(updateCustomerCommand);

        return Ok(updatedCustomer);
    }
}
