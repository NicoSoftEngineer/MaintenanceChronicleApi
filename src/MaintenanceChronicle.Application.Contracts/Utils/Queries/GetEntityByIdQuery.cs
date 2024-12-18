using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Queries;

public record GetEntityByIdQuery<TEntity>(Guid Id) : IRequest<TEntity>;
