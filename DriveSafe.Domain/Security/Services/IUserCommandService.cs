using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Security.Models.Commands;

namespace DriveSafe.Domain.Publishing.Services;

public interface IUserCommandService
{
    Task<User> Handle(SignUpCommand command);
    Task<string> Handle(SignInCommand command);
    Task<int> Handle(CreateUserCommand command);
    Task<Boolean> Handle(int id, UpdateUserCommand command);
    Task<Boolean> Handle(DeleteUserCommand command);
}