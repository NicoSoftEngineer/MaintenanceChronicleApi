using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;

/// <summary>
/// Query to get list of contacts for a location.
/// </summary>
/// <param name="LocationId">ID of location for which to get contacts</param>
/// <returns>List of <see cref="LocationContactInListDto"/> for desired location</returns>
public record GetListOfContactsForLocationQuery(Guid LocationId) : IRequest<List<LocationContactInListDto>>;
