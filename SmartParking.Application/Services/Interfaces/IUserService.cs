using SmartParking.Application.Dtos;

namespace SmartParking.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(RegisterRequestDto request);
    Task<string?> LoginAsync(LoginRequestDto request);
}