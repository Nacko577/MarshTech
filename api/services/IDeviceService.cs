using DeviceManagement.API.DTOs;

namespace DeviceManagement.API.Services;

public interface IDeviceService
{
    Task<IEnumerable<DeviceDto>> GetAllAsync();
    Task<DeviceDto?> GetByIdAsync(int id);
    Task<(DeviceDto? Device, string? Error)> CreateAsync(DeviceWriteDto dto);
    Task<(DeviceDto? Device, string? Error)> UpdateAsync(int id, DeviceWriteDto dto);
    Task<bool> DeleteAsync(int id);
}
