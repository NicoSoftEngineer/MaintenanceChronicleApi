using MediatR;
using System.Security.Claims;
using ServiceTrack.Application.Contracts.Tenants.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Tenants.Commands;

public record AddTenantClaimToUserPrincipalCommand(UserTenantClaimDto UserTenantClaim, ClaimsPrincipal ClaimsPrincipal) : IRequest<ClaimsPrincipal>;
