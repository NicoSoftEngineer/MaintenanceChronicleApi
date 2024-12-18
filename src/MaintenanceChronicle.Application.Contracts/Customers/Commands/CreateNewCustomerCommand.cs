using MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands;

public record CreateNewCustomerCommand(NewCustomerDto NewCustomer, string UserId, string TenantId) : IRequest<Guid>;
