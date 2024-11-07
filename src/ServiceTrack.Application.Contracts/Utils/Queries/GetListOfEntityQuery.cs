using MediatR;

namespace ServiceTrack.Application.Contracts.Utils.Queries;

public record GetListOfEntityQuery<TEntity>() : IRequest<List<TEntity>>;
