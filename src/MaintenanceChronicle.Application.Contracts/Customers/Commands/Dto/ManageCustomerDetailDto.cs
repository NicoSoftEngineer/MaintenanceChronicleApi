using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;

public class ManageCustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name{ get; set; }
}

public static class ManageCustomerDetailDtoExtensions
{
    public static ManageCustomerDetailDto ToManageCustomerDetailDto(this Customer customer)
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
