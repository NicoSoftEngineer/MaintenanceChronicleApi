using MediatR;
using ServiceTrack.Application.Contracts.UserTenant.Commands.Dto;

namespace ServiceTrack.Application.Contracts.UserTenant.Commands;

public record RegisterUserAndTenantCommand(UserTenantDto UserTenantDto) : IRequest<UserTenantIdsDto>;
