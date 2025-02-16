namespace MaintenanceChronicle.Application.Contracts.Customers.Commands.Dto;

public class NewCustomerDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CompanyIdNumber { get; set; }
}
