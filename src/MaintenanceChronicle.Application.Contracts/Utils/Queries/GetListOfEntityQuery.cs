using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Queries;

public record GetListOfEntityQuery<TEntity>() : IRequest<List<TEntity>>;
