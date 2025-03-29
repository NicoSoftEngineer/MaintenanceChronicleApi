using MaintenanceChronicle.Application.Contracts.UserTenant.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.UserTenant.Commands;
/// <summary>
/// Command to register a user and tenant. Uses atomic transaction to register both at the same time.
/// </summary>
/// <param name="RegisterUserTenantDto">Dto with user and tenant info</param>
/// <returns>Dto with user and tenant ID</returns>
public record RegisterUserAndTenantCommand(RegisterUserTenantDto RegisterUserTenantDto) : IRequest<UserTenantIdsDto>;
