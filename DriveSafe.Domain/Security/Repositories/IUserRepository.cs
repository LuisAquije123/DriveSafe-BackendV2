using DriveSafe.Domain.Publishing.Models.Entities;

namespace DriveSafe.Domain.Publishing.Repositories;

public interface IUserRepository
{
    Task<User> Register(User user);
    
    Task<User> Login(string gmail, string password);
    
    Task<int> SaveAsync(User data);
    
    Task<List<User>> GetAllAsync();
    
    Task<User> GetByIdAsync(int id);
    
    Task<Boolean> UpdateAsync(User data, int id);
    
    Task<Boolean> DeleteAsync(int id);
    
    Task<bool> IsEmailInUseAsync(string email);
    
    Task<User> GetUserByGmailAsync(string email);
}