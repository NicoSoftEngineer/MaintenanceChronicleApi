using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrack.Application.Contracts.Customers.Commands;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Customers.Queries.Dto;
using ServiceTrack.Application.Contracts.Users.Commands.Dto;
using ServiceTrack.Application.Contracts.Users.Commands;
using ServiceTrack.Application.Contracts.Utils.Queries;
using ServiceTrack.Utilities.Helpers;
using ServiceTrack.Utilities.Constants;
using ServiceTrack.Application.Contracts.Users.Queries.Dto;
using ServiceTrack.Application.Contracts.Utils.Commands;
using ServiceTrack.Data.Entities.Business;

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
    public async Task<ActionResult<Guid>> CreateCustomer(
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
    /// <param name="patch">Information that admin provides</param>
    /// <returns></returns>
    [HttpPatch("api/v1/customers/{id:guid}")]
    public async Task<ActionResult<ManageCustomerDetailDto>> UpdateCustomer(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<ManageCustomerDetailDto> patch
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

    /// <summary>
    /// Updates customer with the given information
    /// </summary>
    /// <returns></returns>
    [HttpGet("api/v1/customers")]
    public async Task<ActionResult<List<ManageCustomerDetailDto>>> GetCustomerList()
    {
        var getCustomerListQuery = new GetListOfEntityQuery<CustomerInListDto>();
        var customers = await mediator.Send(getCustomerListQuery);

        return Ok(customers);
    }

    /// <summary>
    /// Gets a specific customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>customer</returns>
    [HttpGet("api/v1/customers/{id:guid}")]
    public async Task<ActionResult> GetCustomer(
        [FromRoute] Guid id
    )
    {
        var customerQuery = new GetEntityByIdQuery<CustomerDetailDto>(id);
        var customer = await mediator.Send(customerQuery);

        return Ok(customer);
    }

    /// <summary>
    /// Deletes a specific customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>customer</returns>
    [HttpDelete("api/v1/customers/{id:guid}")]
    public async Task<ActionResult> DeleteCustomer(
        [FromRoute] Guid id
    )
    {
        var deleteCommand = new DeleteEntityByIdCommand<Customer>(id, User.GetUserId());
        await mediator.Send(deleteCommand);

        return NoContent();
    }
}
