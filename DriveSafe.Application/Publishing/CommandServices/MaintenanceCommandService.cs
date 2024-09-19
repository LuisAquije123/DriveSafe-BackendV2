using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.Publishing.CommandServices;

public class MaintenanceCommandService : IMaintenanceCommandService
{
    public readonly IMaintenanceRepository _maintenanceRepository;
    public readonly IMapper _mapper;
    
    public MaintenanceCommandService(IMaintenanceRepository maintenanceRepository, IMapper mapper)
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateMaintenanceCommand command)
    {
        var maintenance = _mapper.Map<CreateMaintenanceCommand, Maintenance>(command);
        
        return await _maintenanceRepository.SaveAsync(maintenance);
    }
}