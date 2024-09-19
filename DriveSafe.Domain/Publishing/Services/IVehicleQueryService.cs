using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;

namespace DriveSafe.Domain.Publishing.Services;

public interface IVehicleQueryService
{
    Task<List<VehicleResponse>?> Handle(GetAllVehiclesQuery query);
    
    Task<VehicleResponse?> Handle(GetVehicleByIdQuery query);
    
    Task<List<VehicleResponse?>> Handle(GetVehicleByUserIdQuery query);
}