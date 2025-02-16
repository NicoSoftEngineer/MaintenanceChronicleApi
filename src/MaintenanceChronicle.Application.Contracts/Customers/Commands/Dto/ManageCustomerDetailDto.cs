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

public static class ManageCustomerDetailDtoExtensions
{
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

    public static void MapToEntity(this ManageCustomerDetailDto dto, Customer target)
    {
        target.Id = dto.Id;
        target.Name = dto.Name;
        target.Email = dto.Email;
        target.PhoneNumber = dto.PhoneNumber;
        target.CompanyIdNumber = dto.CompanyIdNumber;
    }
}
