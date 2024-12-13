using MediatR;

namespace ServiceTrack.Application.Contracts.Utils.Commands;

public record DeleteEntityByIdCommand<TEntity>(Guid Id, string UserId) : IRequest;
