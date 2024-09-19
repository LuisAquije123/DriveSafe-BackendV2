namespace DriveSafe.Domain.Publishing.Models.Response;

public class MaintenanceResponse
{
    public int Id { get; set; }
    public string TypeProblem { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TenantId { get; set; }
    public int OwnerId { get; set; }
}