using MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Machines.Queries;
/// <summary>
/// Query to get all machines for a location.
/// </summary>
/// <param name="LocationId">ID of desired location</param>
/// <returns>List of <see cref="MachineInListForLocationDto"/> from location</returns>
public record GetMachinesForLocationQuery(Guid LocationId) : IRequest<List<MachineInListForLocationDto>>;
