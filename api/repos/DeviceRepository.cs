using DeviceManagement.API.Data;
using DeviceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.API.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Device>> GetAllAsync()
        => await _db.Devices
                    .AsNoTracking()
                    .Include(d => d.User)
                    .ToListAsync();

    public async Task<Device?> GetByIdAsync(int id)
        => await _db.Devices
                    .AsNoTracking()
                    .Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<bool> ExistsAsync(string name, string manufacturer, int? excludeId = null)
        => await _db.Devices.AnyAsync(d =>
            d.Name == name &&
            d.Manufacturer == manufacturer &&
            (excludeId == null || d.Id != excludeId));

    public async Task<Device> CreateAsync(Device device)
    {
        _db.Devices.Add(device);
        await _db.SaveChangesAsync();
        return device;
    }

    public async Task<Device?> UpdateAsync(int id, Device updated)
    {
        var existing = await _db.Devices.FindAsync(id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Manufacturer = updated.Manufacturer;
        existing.Type = updated.Type;
        existing.OperatingSystem = updated.OperatingSystem;
        existing.OsVersion = updated.OsVersion;
        existing.Processor = updated.Processor;
        existing.RamAmount = updated.RamAmount;
        existing.Description = updated.Description;
        existing.UserId = updated.UserId;

        await _db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Devices.FindAsync(id);
        if (existing is null) return false;

        _db.Devices.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
