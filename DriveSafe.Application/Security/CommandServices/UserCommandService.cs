using System.Data;
using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;
using DriveSafe.Domain.Security.Models.Commands;
using DriveSafe.Domain.Security.Services;

namespace DriveSafe.Application.Security.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;
    public UserCommandService(IUserRepository userRepository, IMapper mapper, IEncryptService encryptService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _encryptService = encryptService;
        _tokenService = tokenService;
    }
    
    public async Task<User> Handle(SignUpCommand command)
    {
        var existingUser = await _userRepository.IsEmailInUseAsync(command.Gmail);
        if (existingUser) throw new DuplicateNameException("User already exists");
        var user = _mapper.Map<SignUpCommand, User>(command);
        user.Password = _encryptService.Encrypt(command.Password);
        return await _userRepository.Register(user);
    }

    public async Task<string> Handle(SignInCommand command)
    {
        var existingUser = await _userRepository.IsEmailInUseAsync(command.Gmail);
        if (existingUser != true) throw new DuplicateNameException("User not found");
        
        var user = await _userRepository.GetUserByGmailAsync(command.Gmail);

        if (!_encryptService.VerifyPassword(command.Password, user.Password))
            throw new DuplicateNameException("Invalid password or username");
        
        var token = _tokenService.GenerateToken(user, user.Id);
        return token;
    }
    
    public async Task<int> Handle(CreateUserCommand command)
    {
        var user = _mapper.Map<CreateUserCommand, User>(command);
        
        var existingUser = await _userRepository.IsEmailInUseAsync(command.Gmail);
        
        return await _userRepository.SaveAsync(user);
    }

    public async Task<Boolean> Handle(int id, UpdateUserCommand command)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        var user = _mapper.Map<UpdateUserCommand, User>(command);
        if (existingUser == null) throw new ConstraintException("User not found");
        user.Password = _encryptService.Encrypt(command.Password);
        return await _userRepository.UpdateAsync(user, id);
    }

    public async Task<Boolean> Handle(DeleteUserCommand command)
    {
        var existingUser = _userRepository.GetByIdAsync(command.Id);
        if (existingUser == null) throw new ConstraintException("User not found");
        return await _userRepository.DeleteAsync(command.Id);
    }
}