using ServiceTrack.Data.Entities.Business;

namespace ServiceTrack.Application.Contracts.Customers.Commands.Dto;

public class CustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name{ get; set; }
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

    public static Customer ToCustomerEntity(this CustomerDetailDto customerDto)
    {
        return new Customer
        {
            Id = customerDto.Id,
            Name = customerDto.Name
        };
    }
}
