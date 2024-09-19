using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Infraestructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace DriveSafe.Infraestructure.Publishing.Persistence;

public class RentRepository : IRentRepository
{
    private readonly DriveSafeDBContext _driveSafeDbContext;
    
    public RentRepository(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbContext = driveSafeDbContext;
    }
    
    public async Task<List<Rent>> GetAllAsync()
    {
        var result = await _driveSafeDbContext.Rents.Where(r => r.IsActive).ToListAsync();
        return result;
    }

    public async Task<Rent> GetByIdAsync(int id)
    {
        return await _driveSafeDbContext.Rents.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Rent? data)
    {
        await using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                if (data != null)
                {
                    data.IsActive = true;
                    _driveSafeDbContext.Rents.Add(data);
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

    public async Task<bool> UpdateAsync(Rent data, int id)
    {
        var existingRent = _driveSafeDbContext.Rents.FirstOrDefault(r => r.Id == id);
        if (existingRent != null)
        {
            existingRent.Status = data.Status;
            existingRent.StartDate = data.StartDate;
            existingRent.EndDate = data.EndDate;
            existingRent.VehicleId = data.VehicleId;
            existingRent.OwnerId = data.OwnerId;
            existingRent.TenantId = data.TenantId;
            existingRent.PickUpPlace = data.PickUpPlace;
            _driveSafeDbContext.Rents.Update(existingRent);
        }
        await _driveSafeDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingRent = _driveSafeDbContext.Rents.FirstOrDefault(r => r.Id == id);
        if (existingRent != null)
        {
            _driveSafeDbContext.Rents.Remove(existingRent);
        }
        await _driveSafeDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Rent>> GetByUserIdAsync(int userId)
    {
        return await _driveSafeDbContext.Rents.Where(r => r.TenantId == userId).ToListAsync();
    }
}