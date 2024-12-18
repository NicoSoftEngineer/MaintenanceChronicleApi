using MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands;

public record UpdateCustomerCommand(JsonPatchDocument<ManageCustomerDetailDto> Patch, Guid CustomerId, string UserId) : IRequest<ManageCustomerDetailDto>;
