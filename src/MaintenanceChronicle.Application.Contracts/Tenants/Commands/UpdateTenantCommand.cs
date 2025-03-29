using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;

/// <summary>
/// Command to update tenant details.
/// </summary>
/// <param name="Id">ID of tenant to update</param>
/// <param name="TenantDetail"><see cref="JsonPatchDocument{TenantDetailDto}"/> with instructions on what to change</param>
/// <param name="UserId">ID of requesting user</param>
public record UpdateTenantCommand(Guid Id,JsonPatchDocument<TenantDetailDto> TenantDetail, string UserId) : IRequest<TenantDetailDto>;
