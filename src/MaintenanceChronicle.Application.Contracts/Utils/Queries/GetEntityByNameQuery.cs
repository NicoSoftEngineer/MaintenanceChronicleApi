using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Queries;

public record GetEntityByNameQuery<TEntity>(string Name) : IRequest<TEntity>;
