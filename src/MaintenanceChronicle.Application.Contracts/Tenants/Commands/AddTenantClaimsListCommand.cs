using System.Security.Claims;
using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;
/// <summary>
/// Command to add tenant claim to users list of claims.
/// </summary>
/// <param name="UserTenantClaim">Dto with user email and tenant id</param>
/// <param name="Claims">Existing claims to add to</param>
/// <returns>The updated list of claims</returns>
public record AddTenantClaimsListCommand(UserTenantClaimDto UserTenantClaim, List<Claim> Claims) : IRequest<List<Claim>>;
