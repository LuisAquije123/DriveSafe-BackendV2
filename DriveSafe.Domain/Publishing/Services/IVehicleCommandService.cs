using DriveSafe.Domain.Publishing.Models.Commands;

namespace DriveSafe.Domain.Publishing.Services;

public interface IVehicleCommandService
{
    Task<int> Handle(CreateVehicleCommand command);
    
    Task<bool> Handle(UpdateVehicleCommand command);
    
    Task<bool> Handle(DeleteVehicleCommand command);
}