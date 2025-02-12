using MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.UserTenant.Commands;

public record RegisterUserAndTenantCommand(RegisterUserTenantDto RegisterUserTenantDto) : IRequest<UserTenantIdsDto>;
