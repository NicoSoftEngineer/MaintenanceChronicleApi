using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;

//TODO: Implement Patch
/// <summary>
/// Command to update tenant details.
/// </summary>
/// <remarks>Shouldn't be used, will be remade to implement Patch</remarks>
/// <param name="TenantDetail"></param>
/// <param name="UserId"></param>
public record UpdateTenantCommand(TenantDetailDto TenantDetail, string UserId) : IRequest;
