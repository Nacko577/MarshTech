using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.API.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}

public class UserWriteDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Location { get; set; } = string.Empty;
}
