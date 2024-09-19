using DriveSafe.Domain.Publishing.Models.Entities;

namespace DriveSafe.Domain.Security.Services;

public interface ITokenService
{
    string GenerateToken(User user, int id);
    Task<int?> ValidateToken(string token);
}