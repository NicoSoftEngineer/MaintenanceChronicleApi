using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;

public record CreateNewMaintenanceRecordCommand(NewMaintenanceRecordDto RecordDto, string UserId, string TenantId) : IRequest<Guid>;
