using ServiceTrack.Application.Contracts.Customers.Commands.Dto;
using ServiceTrack.Data.Entities.Business;

namespace ServiceTrack.Application.Contracts.Customers.Queries.Dto;

public class CustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
public static class CustomerDetailDtoExtensions
{
    public static CustomerDetailDto ToCustomerDetailDto(this Customer customer)
    {
        return new CustomerDetailDto
        {
            Id = customer.Id,
            Name = customer.Name
        };
    }

    public static Customer ToCustomerEntity(this CustomerDetailDto manageCustomerDto)
    {
        return new Customer
        {
            Id = manageCustomerDto.Id,
            Name = manageCustomerDto.Name
        };
    }
}
