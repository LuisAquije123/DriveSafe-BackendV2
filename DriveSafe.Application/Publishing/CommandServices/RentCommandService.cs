using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.Publishing.CommandServices;

public class RentCommandService : IRentCommandService
{
    private readonly IRentRepository _rentRepository;
    private readonly IMapper _mapper;
    public RentCommandService(IRentRepository rentRepository, IMapper mapper)
    {
        _rentRepository = rentRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateRentCommand command)
    {
        var rent = _mapper.Map<CreateRentCommand, Rent>(command);
        
        return await _rentRepository.SaveAsync(rent);
    }

    public async Task<bool> Handle(int id, UpdateRentCommand command)
    {
        var existingRent = await _rentRepository.GetByIdAsync(id);
        var rent = _mapper.Map<UpdateRentCommand, Rent>(command);
        if (existingRent == null) return false;
        return await _rentRepository.UpdateAsync(rent, id);
    }

    public async Task<bool> Handle(DeleteRentCommand command)
    {
        var existingRent = _rentRepository.GetByIdAsync(command.Id);
        if (existingRent == null) return false;
        return await _rentRepository.DeleteAsync(command.Id);
    }
}