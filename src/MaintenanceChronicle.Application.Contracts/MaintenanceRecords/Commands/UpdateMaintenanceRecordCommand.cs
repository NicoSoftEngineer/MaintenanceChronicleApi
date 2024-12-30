using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Commands;

public record UpdateMaintenanceRecordCommand(JsonPatchDocument<ManageMaintenanceRecordDetailDto> Patch, Guid Id, string UserId) : IRequest<ManageMaintenanceRecordDetailDto>;
