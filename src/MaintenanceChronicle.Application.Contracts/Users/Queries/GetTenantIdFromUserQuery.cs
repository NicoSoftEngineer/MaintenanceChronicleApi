using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries;

public record GetTenantIdFromUserQuery(string Email) : IRequest<Guid>;
