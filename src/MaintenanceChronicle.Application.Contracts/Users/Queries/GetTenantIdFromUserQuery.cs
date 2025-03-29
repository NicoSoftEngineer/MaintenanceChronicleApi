using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries;
/// <summary>
/// Query to get tenant id from db for user.
/// </summary>
/// <param name="Email">Email of user</param>
/// <returns>Tenant ID</returns>
public record GetTenantIdFromUserQuery(string Email) : IRequest<Guid>;
