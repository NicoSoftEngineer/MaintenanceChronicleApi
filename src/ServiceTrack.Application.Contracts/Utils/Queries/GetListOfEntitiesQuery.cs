using MediatR;

namespace ServiceTrack.Application.Contracts.Utils.Queries;

public record GetListOfEntitiesQuery<TEntity>() : IRequest<List<TEntity>>;
