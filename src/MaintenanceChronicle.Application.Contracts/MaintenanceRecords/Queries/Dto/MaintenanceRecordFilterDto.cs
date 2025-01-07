namespace MaintenanceChronicle.Application.Contracts.MaintenanceRecords.Queries.Dto;

public class MaintenanceRecordFilterDto
{
    public string? SearchText { get; set; }
    public Guid Machine { get; set; }
    public Guid Location { get; set; }
    public int Type { get; set; }
}
