using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Infraestructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace DriveSafe.Infraestructure.Publishing.Persistence;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly DriveSafeDBContext _driveSafeDbContext;
    
    public MaintenanceRepository(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbContext = driveSafeDbContext;
    }
    
    public async Task<List<Maintenance>> GetAllAsync()
    {
        var result = await _driveSafeDbContext.Maintenances.Where(m => m.IsActive).ToListAsync();
        return result;
    }

    public async Task<Maintenance> GetByIdAsync(int id)
    {
        return await _driveSafeDbContext.Maintenances.Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Maintenance data)
    {
        await using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                if (data != null)
                {
                    data.IsActive = true;
                    _driveSafeDbContext.Maintenances.Add(data);
                }
                await _driveSafeDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        if (data != null) return data.Id;
        return 0;
    }
}