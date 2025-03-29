using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries;
/// <summary>
/// Query to get all locations for a customer.
/// </summary>
/// <param name="CustomerId">ID of customer for which to get locations</param>
/// <returns>List of <see cref="LocationInListDto"/> which belong to the desired customer</returns>
public record GetLocationsForCustomerQuery(Guid CustomerId) : IRequest<List<LocationInListDto>>;
