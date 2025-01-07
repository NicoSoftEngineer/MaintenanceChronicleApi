using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;

public record GetFilteredMaintenanceRecordsQuery(MaintenanceRecordFilterDto Filter) : IRequest<List<MaintenanceRecordInListDto>>;
