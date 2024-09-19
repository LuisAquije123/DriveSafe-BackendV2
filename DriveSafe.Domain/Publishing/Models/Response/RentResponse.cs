namespace DriveSafe.Domain.Publishing.Models.Response;

public class RentResponse
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int VehicleId { get; set; }
    public int OwnerId { get; set; }
    public int TenantId { get; set; }
    public string PickUpPlace { get; set; }
}