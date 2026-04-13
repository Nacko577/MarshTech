using DeviceManagement.API.DTOs;
using DeviceManagement.API.Models;
using DeviceManagement.API.Repositories;

namespace DeviceManagement.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return users.Select(ToDto);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user is null ? null : ToDto(user);
    }

    public async Task<UserDto> CreateAsync(UserWriteDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Role = dto.Role,
            Location = dto.Location
        };

        var created = await _repo.CreateAsync(user);
        return ToDto(created);
    }

    public async Task<UserDto?> UpdateAsync(int id, UserWriteDto dto)
    {
        var updated = new User
        {
            Name = dto.Name,
            Role = dto.Role,
            Location = dto.Location
        };

        var result = await _repo.UpdateAsync(id, updated);
        return result is null ? null : ToDto(result);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    private static UserDto ToDto(User u) => new()
    {
        Id = u.Id,
        Name = u.Name,
        Role = u.Role,
        Location = u.Location
    };
}
