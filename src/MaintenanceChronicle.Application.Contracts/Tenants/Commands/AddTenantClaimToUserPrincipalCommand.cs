using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;

public record AddTenantClaimToUserPrincipalCommand(UserTenantClaimDto UserTenantClaim, ClaimsPrincipal ClaimsPrincipal) : IRequest<ClaimsPrincipal>;
