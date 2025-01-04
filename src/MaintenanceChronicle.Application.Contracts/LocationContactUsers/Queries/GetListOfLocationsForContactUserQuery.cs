using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;

public record GetListOfLocationsForContactUserQuery(Guid UserId) : IRequest<List<LocationInListForContactDto>>;
