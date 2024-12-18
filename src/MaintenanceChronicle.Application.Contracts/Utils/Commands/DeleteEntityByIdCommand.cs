using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Utils.Commands;

public record DeleteEntityByIdCommand<TEntity>(Guid Id, string UserId) : IRequest;
