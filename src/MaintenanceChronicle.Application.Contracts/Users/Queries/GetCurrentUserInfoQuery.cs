using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries;
/// <summary>
/// Query to get the current user's information.
/// </summary>
/// <param name="UserId">Users ID</param>
/// <returns>Dto with user info</returns>
public record GetCurrentUserInfoQuery(string UserId) : IRequest<LoggedInUserInfoDto>;
