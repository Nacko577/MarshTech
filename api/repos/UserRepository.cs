using DeviceManagement.API.Data;
using DeviceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _db.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetByIdAsync(int id)
        => await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User updated)
    {
        var existing = await _db.Users.FindAsync(id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Role = updated.Role;
        existing.Location = updated.Location;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Users.FindAsync(id);
        if (existing is null) return false;

        _db.Users.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
