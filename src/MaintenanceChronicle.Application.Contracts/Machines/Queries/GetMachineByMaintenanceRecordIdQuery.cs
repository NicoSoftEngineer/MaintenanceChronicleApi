using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries;

/// <summary>
/// Query to get machine by maintenance record ID.
/// </summary>
/// <param name="RecordId">ID of record for which the machine is requested</param>
/// <returns><see cref="MachineInMaintenanceRecordDetailDto"/> for record</returns>
public record GetMachineByMaintenanceRecordIdQuery(Guid RecordId) : IRequest<MachineInMaintenanceRecordDetailDto>;
