using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;

public class CustomerInListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CompanyIdNumber { get; set; }
}

public static class CustomerListDtoExtensions
{
    public static CustomerInListDto ToListDto(this Customer customer)
    {
        return new CustomerInListDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CompanyIdNumber = customer.CompanyIdNumber
        };
    }
}
