using MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Customers.Queries;
/// <summary>
/// Query to get customer detail for a location.
/// </summary>
/// <param name="LocationId">ID of location for which the customer is requested</param>
/// <returns><see cref="CustomerDetailForLocationDto"/> for the requested location</returns>
public record GetCustomerDetailForLocationQuery(Guid LocationId) : IRequest<CustomerDetailForLocationDto>;
