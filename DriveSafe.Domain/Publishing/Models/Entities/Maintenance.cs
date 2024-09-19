namespace DriveSafe.Domain.Publishing.Models.Entities;

public class Maintenance : BaseModel
{
    public string TypeProblem { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TenantId { get; set; }
    public int OwnerId { get; set; }
}
