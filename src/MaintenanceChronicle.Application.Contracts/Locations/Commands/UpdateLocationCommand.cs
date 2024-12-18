using MaintenanceChronicle.Application.Contracts.Locations.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Locations.Commands;

public record UpdateLocationCommand(JsonPatchDocument<ManageLocationDetailDto> Patch, Guid LocationId, string UserId) : IRequest<ManageLocationDetailDto>;
