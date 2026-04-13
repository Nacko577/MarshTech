using DeviceManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Role).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Location).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("Devices");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(150);
            entity.Property(d => d.Manufacturer).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Type).IsRequired();
            entity.Property(d => d.OperatingSystem).IsRequired().HasMaxLength(50);
            entity.Property(d => d.OsVersion).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Processor).IsRequired().HasMaxLength(100);
            entity.Property(d => d.RamAmount).IsRequired().HasMaxLength(20);
            entity.Property(d => d.Description).HasMaxLength(500);

            entity.HasOne(d => d.User)
                  .WithMany(u => u.Devices)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired(false);
        });
    }
}
