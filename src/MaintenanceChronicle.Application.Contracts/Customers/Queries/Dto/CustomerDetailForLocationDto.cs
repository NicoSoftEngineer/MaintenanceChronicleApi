using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Customers.Queries.Dto;

public class CustomerDetailForLocationDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CompanyIdNumber { get; set; }
}
/// <summary>
/// Extension methods for <see cref="CustomerDetailForLocationDto"/> entity.
/// </summary>
public static class CustomerDetailForLocationDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Customer"/> to <see cref="CustomerDetailForLocationDto"/>.
    /// </summary>
    /// <param name="customer"><see cref="Customer"/> to convert</param>
    /// <returns>Converted entity to <see cref="CustomerDetailForLocationDto"/></returns>
    public static CustomerDetailForLocationDto ToCustomerDetailForLocationDto(this Customer customer)
    {
        return new CustomerDetailForLocationDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CompanyIdNumber = customer.CompanyIdNumber
        };
    }
    /// <summary>
    /// Converts <see cref="CustomerDetailForLocationDto"/> to <see cref="Customer"/>.
    /// </summary>
    /// <param name="manageCustomerDto"> to convert</param>
    /// <returns>Converted entity to <see cref="Customer"/></returns>
    public static Customer ToCustomerEntity(this CustomerDetailForLocationDto manageCustomerDto)
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
