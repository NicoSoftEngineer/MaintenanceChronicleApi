using MediatR;

namespace ServiceTrack.Application.Contracts.Utils.Queries;

public record GetEntityByIdQuery<TEntity>(Guid Id) : IRequest<TEntity>;
