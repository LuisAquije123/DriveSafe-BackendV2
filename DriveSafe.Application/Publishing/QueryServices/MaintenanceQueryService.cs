using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.QueryServices;

public class MaintenanceQueryService : IMaintenanceQueryService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IMapper _mapper;
    
    public MaintenanceQueryService(IMaintenanceRepository maintenanceRepository, IMapper mapper)
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
    }
    
    public async Task<List<MaintenanceResponse>?> Handle(GetAllMaintenancesQuery query)
    {
        var data = await _maintenanceRepository.GetAllAsync();
        var result = _mapper.Map<List<Maintenance>, List<MaintenanceResponse>>(data);
        return result;
    }

    public async Task<MaintenanceResponse?> Handle(GetMaintenanceByIdQuery query)
    {
        var data = await _maintenanceRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Maintenance, MaintenanceResponse>(data);
        return result;
    }
}