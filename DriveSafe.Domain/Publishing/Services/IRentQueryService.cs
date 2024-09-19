using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;

namespace DriveSafe.Domain.Publishing.Services;

public interface IRentQueryService
{
    Task<List<RentResponse>?> Handle(GetAllRentsQuery query);
    Task<RentResponse?> Handle(GetRentByIdQuery query);
    Task<List<RentResponse?>> Handle(GetRentByUserIdQuery query);
}