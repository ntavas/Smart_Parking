using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Services.Interfaces;
using SmartParking.Domain.Entities;
using SmartParking.Infrastructure.Persistence;

namespace SmartParking.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SmpDbContext _context;

    public UserRepository(SmpDbContext context)
    {
        _context = context;    
    }

    public async Task<User?> GetUserByEmailAsync(string email) 
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}