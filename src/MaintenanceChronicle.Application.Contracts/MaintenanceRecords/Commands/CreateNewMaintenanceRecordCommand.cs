using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;
/// <summary>
/// Command to create a new maintenance record.
/// </summary>
/// <param name="RecordDto"><see cref="NewMaintenanceRecordDto"/> with new record info</param>
/// <param name="UserId">ID of user who created the record</param>
/// <param name="TenantId">ID of tenant who is responsible</param>
public record CreateNewMaintenanceRecordCommand(NewMaintenanceRecordDto RecordDto, string UserId, string TenantId) : IRequest<Guid>;
