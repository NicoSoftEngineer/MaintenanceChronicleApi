using MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands;
/// <summary>
/// Command to update a customer.
/// </summary>
/// <param name="Patch"><see cref="JsonPatchDocument"/> with instructions on what to replace</param>
/// <param name="CustomerId">ID of customer to update</param>
/// <param name="UserId">ID of requesting user</param>
public record UpdateCustomerCommand(JsonPatchDocument<ManageCustomerDetailDto> Patch, Guid CustomerId, string UserId) : IRequest<ManageCustomerDetailDto>;
