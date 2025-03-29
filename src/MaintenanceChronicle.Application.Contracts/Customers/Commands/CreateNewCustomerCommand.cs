using MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands;
/// <summary>
/// Command to create a new customer.
/// </summary>
/// <param name="NewCustomer"><see cref="NewCustomerDto"/> with data</param>
/// <param name="UserId">ID of requesting user</param>
/// <param name="TenantId">ID of responsible tenant</param>
public record CreateNewCustomerCommand(NewCustomerDto NewCustomer, string UserId, string TenantId) : IRequest<Guid>;
