using MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.LocationContactUsers.Queries;

public record GetListOfContactsForLocationQuery(Guid LocationId) : IRequest<List<LocationContactInListDto>>;
