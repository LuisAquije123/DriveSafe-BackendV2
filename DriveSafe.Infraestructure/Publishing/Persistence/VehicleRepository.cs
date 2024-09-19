using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Infraestructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace DriveSafe.Infraestructure.Publishing.Persistence;

public class VehicleRepository : IVehicleRepository
{
    private readonly DriveSafeDBContext _driveSafeDbContext;
    
    public VehicleRepository(DriveSafeDBContext driveSafeDbContext)
    {
        _driveSafeDbContext = driveSafeDbContext;
    }
    
    public async Task<List<Vehicle>> GetAllAsync()
    {
        var result = await _driveSafeDbContext.Vehicles.Where(v => v.IsActive).ToListAsync();
        return result;
    }

    public async Task<Vehicle> GetByIdAsync(int id)
    {
        return await _driveSafeDbContext.Vehicles.Where(v => v.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Vehicle? data)
    {
        await using (var transaction = await _driveSafeDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                if (data != null)
                {
                    data.IsActive = true;
                    _driveSafeDbContext.Vehicles.Add(data);
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

    public async Task<bool> UpdateAsync(Vehicle data, int id)
    {
        var existingVehicle = _driveSafeDbContext.Vehicles.FirstOrDefault(v => v.Id == id);
        if (existingVehicle != null)
        {
            existingVehicle.Brand = data.Brand;
            existingVehicle.Model = data.Model;
            existingVehicle.MaximumSpeed = data.MaximumSpeed;
            existingVehicle.Consumption = data.Consumption;
            existingVehicle.Dimensions = data.Dimensions;
            existingVehicle.Weight = data.Weight;
            existingVehicle.CarClass = data.CarClass;
            existingVehicle.Transmission = data.Transmission;
            existingVehicle.TimeType = data.TimeType;
            existingVehicle.RentalCost = data.RentalCost;
            existingVehicle.PickUpPlace = data.PickUpPlace;
            existingVehicle.UrlImage = data.UrlImage;
            existingVehicle.RentStatus = data.RentStatus;
            existingVehicle.OwnerId = data.OwnerId;
            existingVehicle.IsActive = data.IsActive;
            
            _driveSafeDbContext.Vehicles.Update(existingVehicle);
        }

        await _driveSafeDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingVehicle = _driveSafeDbContext.Vehicles.FirstOrDefault(v => v.Id == id);
        
        if (existingVehicle != null)
        {
            _driveSafeDbContext.Vehicles.Remove(existingVehicle);
        }
        
        await _driveSafeDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Vehicle>> GetByUserIdAsync(int userId)
    {
        return await _driveSafeDbContext.Vehicles.Where(v => v.OwnerId == userId).ToListAsync();
    }
}