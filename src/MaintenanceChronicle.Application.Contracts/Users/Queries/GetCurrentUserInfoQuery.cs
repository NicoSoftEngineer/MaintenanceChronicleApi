using MaintenanceChronicle.Application.Contracts.Users.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Users.Queries;

public record GetCurrentUserInfoQuery(string UserId) : IRequest<LoggedInUserInfoDto>;
