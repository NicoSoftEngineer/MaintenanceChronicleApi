using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Application.Contracts.Locations.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Locations.Commands;

public record UpdateLocationCommand(JsonPatchDocument<ManageLocationDetailDto> Patch, Guid LocationId, string UserId) : IRequest<ManageLocationDetailDto>;
