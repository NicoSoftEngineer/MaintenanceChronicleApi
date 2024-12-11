using ServiceTrack.Data.Entities.Business;

namespace ServiceTrack.Application.Contracts.Customers.Commands.Dto;

public class ManageCustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name{ get; set; }
}

public static class CustomerDetailDtoExtensions
{
    public static ManageCustomerDetailDto ToCustomerDetailDto(this Customer customer)
    {
        return new ManageCustomerDetailDto
        {
            Id = customer.Id,
            Name = customer.Name
        };
    }

    public static Customer ToCustomerEntity(this ManageCustomerDetailDto manageCustomerDto)
    {
        return new Customer
        {
            Id = manageCustomerDto.Id,
            Name = manageCustomerDto.Name
        };
    }
}
