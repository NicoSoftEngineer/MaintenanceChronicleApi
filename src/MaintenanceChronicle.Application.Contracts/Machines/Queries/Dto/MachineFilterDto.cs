namespace MaintenanceChronicle.Application.Contracts.Machines.Queries.Dto;

public class MachineFilterDto
{
    public string? SearchText { get; set; }
    public Guid LocationId { get; set; }
}
