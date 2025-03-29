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
/// <summary>
/// Extension methods for <see cref="CustomerDetailDto"/> entity.
/// </summary>
public static class CustomerDetailDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Customer"/> to <see cref="CustomerDetailDto"/>.
    /// </summary>
    /// <param name="customer"><see cref="Customer"/> to convert</param>
    /// <returns>Converted entity to <see cref="CustomerDetailDto"/></returns>
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
    /// <summary>
    /// Converts <see cref="CustomerDetailDto"/> to <see cref="Customer"/>.
    /// </summary>
    /// <param name="manageCustomerDto"><see cref="CustomerDetailDto"/> to convert</param>
    /// <returns>Converted entity to <see cref="Customer"/></returns>
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
