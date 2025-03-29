using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;
/// <summary>
/// Command to update maintenance record.
/// </summary>
/// <param name="Patch"><see cref="JsonPatchDocument"/> with instructions on what to replace</param>
/// <param name="Id">ID of record which should get updated</param>
/// <param name="UserId">ID of user who requested the update</param>
public record UpdateMaintenanceRecordCommand(JsonPatchDocument<ManageMaintenanceRecordDetailDto> Patch, Guid Id, string UserId) : IRequest<ManageMaintenanceRecordDetailDto>;
