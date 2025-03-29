using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;
/// <summary>
/// Query to get list of locations for a contact user.
/// </summary>
/// <param name="UserId">ID for which to get the locations</param>
/// <returns>List of <see cref="LocationInListForContactDto"/> for which the user is contact</returns>
public record GetListOfLocationsForContactUserQuery(Guid UserId) : IRequest<List<LocationInListForContactDto>>;
