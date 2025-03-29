using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Commands;
/// <summary>
/// Command to delete an entity by its ID.
/// </summary>
/// <typeparam name="TEntity">Entity type to delete</typeparam>
/// <param name="Id">ID of entity</param>
/// <param name="UserId">UserId of the requesting user</param>
public record DeleteEntityByIdCommand<TEntity>(Guid Id, string UserId) : IRequest;
