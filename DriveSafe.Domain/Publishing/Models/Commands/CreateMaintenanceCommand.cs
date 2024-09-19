using System.ComponentModel.DataAnnotations;

namespace DriveSafe.Domain.Publishing.Models.Commands;

public class CreateMaintenanceCommand
{
    [Required] public string TypeProblem { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int TenantId { get; set; }
    [Required] public int OwnerId { get; set; }
}