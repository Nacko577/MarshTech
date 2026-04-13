using DeviceManagement.API.DTOs;

namespace DeviceManagement.API.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserWriteDto dto);
    Task<UserDto?> UpdateAsync(int id, UserWriteDto dto);
    Task<bool> DeleteAsync(int id);
}
