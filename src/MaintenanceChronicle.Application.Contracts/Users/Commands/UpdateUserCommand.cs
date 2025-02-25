using MaintenanceChronicle.Application.Contracts.Users.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Users.Commands;

public record UpdateUserCommand(JsonPatchDocument<UpdateUserDetailDto> Patch, Guid ModifiedUserId, string UserId) : IRequest;
