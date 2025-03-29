using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Queries;
/// <summary>
/// Query to get an entity by its ID.
/// </summary>
/// <typeparam name="TEntity">Entity type to query</typeparam>
/// <param name="Id">ID of desired entity</param>
/// <returns>Entity with the given ID</returns>
public record GetEntityByIdQuery<TEntity>(Guid Id) : IRequest<TEntity>;
