using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;

public class CustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CompanyIdNumber { get; set; }
}
public static class CustomerDetailDtoExtensions
{
    public static CustomerDetailDto ToCustomerDetailDto(this Customer customer)
    {
        return new CustomerDetailDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CompanyIdNumber = customer.CompanyIdNumber
        };
    }

    public static Customer ToCustomerEntity(this CustomerDetailDto manageCustomerDto)
    {
        return new Customer
        {
            Id = manageCustomerDto.Id,
            Name = manageCustomerDto.Name,
            Email = manageCustomerDto.Email,
            PhoneNumber = manageCustomerDto.PhoneNumber,
            CompanyIdNumber = manageCustomerDto.CompanyIdNumber
        };
    }
}
