using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Models.Response;

namespace DriveSafe.Presentation.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
        CreateMap<Vehicle, VehicleResponse>();
        CreateMap<Rent, RentResponse>();
        CreateMap<Maintenance, MaintenanceResponse>();
    }
}