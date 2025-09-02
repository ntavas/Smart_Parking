using SmartParking.Domain.Entities;

namespace SmartParking.Application.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}