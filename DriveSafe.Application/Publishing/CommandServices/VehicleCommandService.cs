using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.Publishing.CommandServices;

public class VehicleCommandService : IVehicleCommandService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;
    
    public VehicleCommandService(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateVehicleCommand command)
    {
        var vehicle = _mapper.Map<CreateVehicleCommand, Vehicle>(command);
        
        return await _vehicleRepository.SaveAsync(vehicle);
    }

    public async Task<bool> Handle(UpdateVehicleCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Handle(DeleteVehicleCommand command)
    {
        var existingVehicle = await _vehicleRepository.GetByIdAsync(command.Id);
        if (existingVehicle == null) throw new KeyNotFoundException("Vehicle not found");
        return await _vehicleRepository.DeleteAsync(command.Id);
    }
}