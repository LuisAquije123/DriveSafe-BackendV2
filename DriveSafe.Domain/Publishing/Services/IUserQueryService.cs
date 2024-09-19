using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;

namespace DriveSafe.Domain.Publishing.Services;

public interface IUserQueryService
{
    Task<List<UserResponse>?> Handle(GetAllUsersQuery query);
    Task<UserResponse?> Handle(GetUserByIdQuery query);
}