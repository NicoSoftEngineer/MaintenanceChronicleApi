using MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Customers.Queries;

public record GetCustomerDetailForLocationQuery(Guid LocationId) : IRequest<CustomerDetailForLocationDto>;
