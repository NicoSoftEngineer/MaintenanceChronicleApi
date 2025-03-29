using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;
/// <summary>
/// Command to update a location.
/// </summary>
/// <param name="Patch"><see cref="JsonPatchDocument"/> with instructions on what to replace</param>
/// <param name="LocationId">ID of location which should get updated</param>
/// <param name="UserId">ID of requesting user</param>
public record UpdateLocationCommand(JsonPatchDocument<ManageLocationDetailDto> Patch, Guid LocationId, string UserId) : IRequest<ManageLocationDetailDto>;
