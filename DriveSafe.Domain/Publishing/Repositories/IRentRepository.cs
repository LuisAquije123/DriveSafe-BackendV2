using DriveSafe.Domain.Publishing.Models.Entities;

namespace DriveSafe.Domain.Publishing.Repositories;

public interface IRentRepository
{
    Task<List<Rent>> GetAllAsync();
    Task<Rent> GetByIdAsync(int id);
    Task<int> SaveAsync(Rent data);
    Task<bool> UpdateAsync(Rent data, int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Rent>> GetByUserIdAsync(int userId);
}