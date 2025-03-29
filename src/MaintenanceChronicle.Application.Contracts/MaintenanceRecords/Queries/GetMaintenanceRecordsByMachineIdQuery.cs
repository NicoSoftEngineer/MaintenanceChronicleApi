using MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries;
/// <summary>
/// Query to get maintenance records by machine id.
/// </summary>
/// <param name="MachineId">ID of desired machine</param>
/// <returns>List of <see cref="MaintenanceRecordInListForMachineDto"/> from machine</returns>
public record GetMaintenanceRecordsByMachineIdQuery(Guid MachineId) : IRequest<List<MaintenanceRecordInListForMachineDto>>;
