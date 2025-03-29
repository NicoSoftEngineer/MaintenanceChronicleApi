using MaintenanceChronicle.Application.Contracts.Locations.Queries.Dto;
using MediatR;

namespace MaintenanceChronicle.Application.Contracts.Locations.Queries;
/// <summary>
/// Query to get location for a machine.
/// </summary>
/// <param name="MachineId">ID of machine for which to get the location</param>
/// <returns><see cref="LocationInListDto"/> where the machine is located</returns>
public record GetLocationForMachineQuery(Guid MachineId) : IRequest<LocationInListDto>;
