using MediatR;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Customers.Commands;

public record CreateNewCustomerCommand(NewCustomerDto NewCustomer, string UserId, string TenantId) : IRequest<Guid>;
