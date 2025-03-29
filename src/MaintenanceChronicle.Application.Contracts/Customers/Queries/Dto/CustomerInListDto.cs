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
/// <summary>
/// Extension methods for <see cref="CustomerInListDto"/> entity.
/// </summary>
public static class CustomerListDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Customer"/> to <see cref="CustomerInListDto"/>.
    /// </summary>
    /// <param name="customer"><see cref="Customer"/> to convert</param>
    /// <returns>Converted entity to <see cref="CustomerInListDto"/></returns>
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
