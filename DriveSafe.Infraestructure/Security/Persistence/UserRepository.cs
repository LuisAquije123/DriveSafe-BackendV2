using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Infraestructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace DriveSafe.Infraestructure.Publishing.Persistence;

public class UserRepository : IUserRepository
{
    private readonly DriveSafeDBContext _driveSafeDbContext;
    
    public UserRepository(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbContext = driveSafeDbContext;
    }

    public async Task<User> Register(User user)
    {
        _driveSafeDbContext.Users.Add(user);
        await _driveSafeDbContext.SaveChangesAsync();
        return user;
    }

    public Task<User> Login(string gmail, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveAsync(User data)
    {
        data.IsActive = true;
        
        using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _driveSafeDbContext.Users.Add(data);
                await _driveSafeDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return data.Id;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _driveSafeDbContext.Users.Where(u => u.IsActive).ToListAsync();
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        return await _driveSafeDbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Boolean> UpdateAsync(User data, int id)
    {
        using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            var userToUpdate = _driveSafeDbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            userToUpdate.Name = data.Name;
            userToUpdate.LastName = data.LastName;
            userToUpdate.Birthdate = data.Birthdate;
            userToUpdate.Cellphone = data.Cellphone;
            userToUpdate.Gmail = data.Gmail;
            userToUpdate.Password = data.Password;
            
            _driveSafeDbContext.Users.Update(userToUpdate);
            await _driveSafeDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            var userToDelete = await _driveSafeDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToDelete != null)
            {
                _driveSafeDbContext.Users.Remove(userToDelete);
                await _driveSafeDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        return true;
    }

    public async Task<bool> IsEmailInUseAsync(string email)
    {
        return await _driveSafeDbContext.Users.AnyAsync(u => u.Gmail == email);
    }
    public async Task<User> GetUserByGmailAsync(string email)
    {
        return await _driveSafeDbContext.Users.FirstOrDefaultAsync(u => u.Gmail == email);
    }
    
}
