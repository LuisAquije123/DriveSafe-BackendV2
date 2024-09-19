using DriveSafe.Domain.Publishing.Models.Commands;

namespace DriveSafe.Domain.Publishing.Services;

public interface IMaintenanceCommandService
{
    Task<int> Handle(CreateMaintenanceCommand command);
}