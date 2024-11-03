using MediatR;

namespace ServiceTrack.Application.Contracts.Users.Queries;

public record GetTenantIdFromUserCommand(string Email) : IRequest<Guid>;
