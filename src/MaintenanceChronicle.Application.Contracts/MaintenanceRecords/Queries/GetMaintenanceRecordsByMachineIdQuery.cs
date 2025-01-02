using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;

public record GetMaintenanceRecordsByMachineIdQuery(Guid MachineId) : IRequest<List<MaintenanceRecordInListForMachineDto>>;
