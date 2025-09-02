using SmartParking.Domain.Entities;

namespace SmartParking.Application.Services.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
}