using System.ComponentModel.DataAnnotations;
using DeviceManagement.API.Models;

namespace DeviceManagement.API.DTOs;

public class DeviceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string OperatingSystem { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;
    public string Processor { get; set; } = string.Empty;
    public string RamAmount { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? UserId { get; set; }
    public UserDto? User { get; set; }
}

public class DeviceWriteDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;

    [Required]
    public DeviceType Type { get; set; }

    [Required]
    [MaxLength(50)]
    public string OperatingSystem { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string OsVersion { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Processor { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string RamAmount { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int? UserId { get; set; }
}
