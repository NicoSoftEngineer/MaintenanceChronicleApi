using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Queries;
/// <summary>
/// Query to get an entity by its name.
/// </summary>
/// <typeparam name="TEntity">Entity týpe to query</typeparam>
/// <param name="Name">The name of needed entity</param>
/// <returns>The entity with the desired name</returns>
public record GetEntityByNameQuery<TEntity>(string Name) : IRequest<TEntity>;
