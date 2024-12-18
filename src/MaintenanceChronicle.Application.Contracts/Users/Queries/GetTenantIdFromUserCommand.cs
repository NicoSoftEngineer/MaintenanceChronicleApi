using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries;

public record GetTenantIdFromUserCommand(string Email) : IRequest<Guid>;
