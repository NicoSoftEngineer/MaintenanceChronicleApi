using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries;

public record GetMachineByMaintenanceRecordIdQuery(Guid RecordId) : IRequest<MachineInMaintenanceRecordDetailDto>;
