using MediatR;

namespace ServiceTrack.Application.Contracts.Utils.Queries;

public record GetEntityByNameQuery<TEntity>(string Name) : IRequest<TEntity>;
