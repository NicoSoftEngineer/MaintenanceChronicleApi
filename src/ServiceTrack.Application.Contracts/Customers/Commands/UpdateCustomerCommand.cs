using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using ServiceTrack.Application.Contracts.Customers.Commands.Dto;

namespace ServiceTrack.Application.Contracts.Customers.Commands;

public record UpdateCustomerCommand(JsonPatchDocument<CustomerDetailDto> Patch, Guid CustomerId, string UserId) : IRequest<CustomerDetailDto>;
