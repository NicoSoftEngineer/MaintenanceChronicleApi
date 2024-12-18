using MediatR;
using ServiceTrack.Application.Contracts.LocationContactUsers.Queries.Dto;

namespace ServiceTrack.Application.Contracts.LocationContactUsers.Queries;

public record GetListOfContactsForLocationQuery(Guid LocationId) : IRequest<List<LocationContactInListDto>>;
