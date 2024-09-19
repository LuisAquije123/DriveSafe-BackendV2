using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Security.Models.Commands;

namespace DriveSafe.Presentation.Mapper;

public class ModelToRequest : Profile
{
    public ModelToRequest()
    {
        CreateMap<User, SignUpCommand>();
        CreateMap<User, UpdateUserCommand>();
        CreateMap<Vehicle, CreateVehicleCommand>();
        CreateMap<Vehicle, UpdateVehicleCommand>();
        CreateMap<Vehicle, DeleteVehicleCommand>();
        CreateMap<Rent, CreateRentCommand>();
        CreateMap<Rent, UpdateRentCommand>();
        CreateMap<Rent, DeleteRentCommand>();
        CreateMap<Maintenance, CreateMaintenanceCommand>();
    }
}