using SmartParking.Application.Dtos;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Mappings;

public static class UserMapper
{
    public static User ToEntity(this RegisterRequestDto dto)
    {
        return new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = dto.Password,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.Id, user.FullName, user.Email, user.CreatedAt);
    }
}