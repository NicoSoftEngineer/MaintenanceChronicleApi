using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;

public record AddTenantClaimsListCommand(UserTenantClaimDto UserTenantClaim, List<Claim> Claims) : IRequest<List<Claim>>;
