using System.ComponentModel.DataAnnotations;

namespace DriveSafe.Domain.Security.Models.Commands;

public record SignUpCommand()
{
    [Required] public string Name { get; set; }
    
    [Required] public string LastName { get; set; }
    
    [Required] public DateOnly Birthdate { get; set; }
    
    [Required] public int Cellphone { get; set; }
    
    [Required] public string Gmail { get; set; }
    
    [Required] public string Password { get; set; }
    
    [Required] public string Type { get; set; }
}