using DriveSafe.Domain.Publishing.Models.Commands;

namespace DriveSafe.Domain.Publishing.Services;

public interface IRentCommandService
{
    Task<int> Handle(CreateRentCommand command);
    
    Task<bool> Handle(int id, UpdateRentCommand command);
    
    Task<bool> Handle(DeleteRentCommand command);
}