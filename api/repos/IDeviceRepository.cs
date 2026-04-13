using DeviceManagement.API.Models;

namespace DeviceManagement.API.Repositories;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllAsync();
    Task<Device?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(string name, string manufacturer, int? excludeId = null);
    Task<Device> CreateAsync(Device device);
    Task<Device?> UpdateAsync(int id, Device device);
    Task<bool> DeleteAsync(int id);
}
