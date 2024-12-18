using MaintenanceChronicle.Application.Contracts.Tenants.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Tenants.Commands;

public record AssignCreationInfoToTenantCommand(TenantsCreatorUserIdDto TenantsCreatorUserIdDto) : IRequest;  
