using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;

namespace DriveSafe.Domain.Publishing.Services;

public interface IMaintenanceQueryService
{
    Task<List<MaintenanceResponse>?> Handle(GetAllMaintenancesQuery query);
    
    Task<MaintenanceResponse?> Handle(GetMaintenanceByIdQuery query);
}