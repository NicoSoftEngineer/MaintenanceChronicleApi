using MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.UserTenant.Commands;

public record RegisterUserAndTenantCommand(UserTenantDto UserTenantDto) : IRequest<UserTenantIdsDto>;
