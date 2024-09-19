using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.QueryServices;

public class VehicleQueryService : IVehicleQueryService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;
    
    public VehicleQueryService(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }
    
    public async Task<List<VehicleResponse>?> Handle(GetAllVehiclesQuery query)
    {
        var data = await _vehicleRepository.GetAllAsync();
        var result = _mapper.Map<List<Vehicle>, List<VehicleResponse>>(data);
        return result;
    }

    public async Task<VehicleResponse?> Handle(GetVehicleByIdQuery query)
    {
        var data = await _vehicleRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Vehicle, VehicleResponse>(data);
        return result;
    }

    public async Task<List<VehicleResponse?>> Handle(GetVehicleByUserIdQuery query)
    {
        
        var data = await _vehicleRepository.GetByUserIdAsync(query.Id);
        var result = _mapper.Map<List<Vehicle>, List<VehicleResponse>>(data);
        return result;
    }
}