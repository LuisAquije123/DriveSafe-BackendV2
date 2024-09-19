namespace DriveSafe.Domain.Publishing.Models.Commands;

public class UpdateVehicleCommand
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int MaximumSpeed { get; set; }
    public int Consumption { get; set; }
    public string Dimensions { get; set; }
    public int Weight { get; set; }
    public string CarClass { get; set; }
    public string Transmission { get; set; }
    public string TimeType { get; set; }
    public int RentalCost { get; set; }
    public string PickUpPlace { get; set; }
    public string UrlImage { get; set; }
    public string RentStatus { get; set; }
    public int OwnerId { get; set; }
}