using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.QueryServices;

public class RentQueryService : IRentQueryService
{
    private readonly IRentRepository _rentRepository;
    private readonly IMapper _mapper;
    
    public RentQueryService(IRentRepository rentRepository, IMapper mapper)
    {
        _rentRepository = rentRepository;
        _mapper = mapper;
    }
    
    public async Task<List<RentResponse>?> Handle(GetAllRentsQuery query)
    {
        var data = await _rentRepository.GetAllAsync();
        var result = _mapper.Map<List<Rent>, List<RentResponse>>(data);
        return result;
    }
    public async Task<RentResponse?> Handle(GetRentByIdQuery query)
    {
        var data = await _rentRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Rent, RentResponse>(data);
        return result;
    }

    public async Task<List<RentResponse?>> Handle(GetRentByUserIdQuery query)
    {
        var data = await _rentRepository.GetByUserIdAsync(query.Id);
        var result = _mapper.Map<List<Rent>, List<RentResponse>>(data);
        return result;
    }
}