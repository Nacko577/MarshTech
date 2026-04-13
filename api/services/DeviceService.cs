using DeviceManagement.API.DTOs;
using DeviceManagement.API.Models;
using DeviceManagement.API.Repositories;

namespace DeviceManagement.API.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepo;
    private readonly IUserRepository _userRepo;

    public DeviceService(IDeviceRepository deviceRepo, IUserRepository userRepo)
    {
        _deviceRepo = deviceRepo;
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<DeviceDto>> GetAllAsync()
    {
        var devices = await _deviceRepo.GetAllAsync();
        return devices.Select(ToDto);
    }

    public async Task<DeviceDto?> GetByIdAsync(int id)
    {
        var device = await _deviceRepo.GetByIdAsync(id);
        return device is null ? null : ToDto(device);
    }

    public async Task<(DeviceDto? Device, string? Error)> CreateAsync(DeviceWriteDto dto)
    {
        if (await _deviceRepo.ExistsAsync(dto.Name, dto.Manufacturer))
            return (null, $"A device named '{dto.Name}' from '{dto.Manufacturer}' already exists.");

        if (dto.UserId.HasValue && await _userRepo.GetByIdAsync(dto.UserId.Value) is null)
            return (null, $"User with id {dto.UserId} does not exist.");

        var created = await _deviceRepo.CreateAsync(MapWriteDto(dto));
        var full = await _deviceRepo.GetByIdAsync(created.Id);
        return (ToDto(full!), null);
    }

    public async Task<(DeviceDto? Device, string? Error)> UpdateAsync(int id, DeviceWriteDto dto)
    {
        if (await _deviceRepo.ExistsAsync(dto.Name, dto.Manufacturer, excludeId: id))
            return (null, $"Another device named '{dto.Name}' from '{dto.Manufacturer}' already exists.");

        if (dto.UserId.HasValue && await _userRepo.GetByIdAsync(dto.UserId.Value) is null)
            return (null, $"User with id {dto.UserId} does not exist.");

        var updated = await _deviceRepo.UpdateAsync(id, MapWriteDto(dto));
        return updated is null ? (null, null) : (ToDto(updated), null);
    }

    public Task<bool> DeleteAsync(int id) => _deviceRepo.DeleteAsync(id);

    private static Device MapWriteDto(DeviceWriteDto dto) => new()
    {
        Name = dto.Name,
        Manufacturer = dto.Manufacturer,
        Type = dto.Type,
        OperatingSystem = dto.OperatingSystem,
        OsVersion = dto.OsVersion,
        Processor = dto.Processor,
        RamAmount = dto.RamAmount,
        Description = dto.Description,
        UserId = dto.UserId
    };

    private static DeviceDto ToDto(Device d) => new()
    {
        Id = d.Id,
        Name = d.Name,
        Manufacturer = d.Manufacturer,
        Type = d.Type.ToString(),
        OperatingSystem = d.OperatingSystem,
        OsVersion = d.OsVersion,
        Processor = d.Processor,
        RamAmount = d.RamAmount,
        Description = d.Description,
        UserId = d.UserId,
        User = d.User is null ? null : new UserDto
        {
            Id = d.User.Id,
            Name = d.User.Name,
            Role = d.User.Role,
            Location = d.User.Location
        }
    };
}
