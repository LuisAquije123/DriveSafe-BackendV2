using System.ComponentModel.DataAnnotations;

namespace DriveSafe.Domain.Publishing.Models.Commands;

public class CreateVehicleCommand
{
    [Required] public string Brand { get; set; }
    [Required] public string Model { get; set; }
    [Required] public int MaximumSpeed { get; set; }
    [Required] public int Consumption { get; set; }
    [Required] public string Dimensions { get; set; }
    [Required] public int Weight { get; set; }
    [Required] public string CarClass { get; set; }
    [Required] public string Transmission { get; set; }
    [Required] public string TimeType { get; set; }
    [Required] public int RentalCost { get; set; }
    [Required] public string PickUpPlace { get; set; }
    [Required] public string UrlImage { get; set; }
    [Required] public string RentStatus { get; set; }
    [Required] public int OwnerId { get; set; }
}