using MaintenanceChronicle.Application.Contracts.Machines.Commands.Dto;
using MaintenanceChronicle.Data.Entities.Business;

namespace MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;

public class ManageCustomerDetailDto
{
    public Guid Id { get; set; }
    public required string Name{ get; set; }
    public required string Email{ get; set; }
    public required string PhoneNumber{ get; set; }
    public required string CompanyIdNumber{ get; set; }
}
/// <summary>
/// Extension methods for <see cref="ManageCustomerDetailDto"/> entity.
/// </summary>
public static class ManageCustomerDetailDtoExtensions
{
    /// <summary>
    /// Converts <see cref="Customer"/> to <see cref="ManageCustomerDetailDto"/>.
    /// </summary>
    /// <param name="customer"><see cref="Customer"/> to convert</param>
    /// <returns>Converted entity to <see cref="ManageCustomerDetailDto"/></returns>
    public static ManageCustomerDetailDto ToManageCustomerDetailDto(this Customer customer)
    {
        return new ManageCustomerDetailDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber= customer.PhoneNumber,
            CompanyIdNumber = customer.CompanyIdNumber
        };
    }
    /// <summary>
    /// Converts <see cref="ManageCustomerDetailDto"/> to <see cref="Customer"/>.
    /// </summary>
    /// <param name="manageCustomerDto"><see cref="ManageCustomerDetailDto"/> to convert</param>
    /// <returns>Converted entity to <see cref="Customer"/></returns>
    public static Customer ToCustomerEntity(this ManageCustomerDetailDto manageCustomerDto)
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
    /// <summary>
    /// Maps properties of <see cref="ManageCustomerDetailDto"/> to <see cref="Customer"/>.
    /// </summary>
    /// <param name="dto">Source <see cref="ManageCustomerDetailDto"/></param>
    /// <param name="target">Destination <see cref="Customer"/></param>
    public static void MapToEntity(this ManageCustomerDetailDto dto, Customer target)
    {
        target.Id = dto.Id;
        target.Name = dto.Name;
        target.Email = dto.Email;
        target.PhoneNumber = dto.PhoneNumber;
        target.CompanyIdNumber = dto.CompanyIdNumber;
    }
}
