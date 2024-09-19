namespace DriveSafe.Domain.Publishing.Models.Entities;

public class BaseModel
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
}