namespace DeviceManagement.API.Models;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
    public string OperatingSystem { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;
    public string Processor { get; set; } = string.Empty;
    public string RamAmount { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }
}
