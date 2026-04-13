using DeviceManagement.API.DTOs;
using DeviceManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _service;

    public DevicesController(IDeviceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var devices = await _service.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await _service.GetByIdAsync(id);
        return device is null ? NotFound() : Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeviceWriteDto dto)
    {
        var (device, error) = await _service.CreateAsync(dto);

        if (error is not null)
            return Conflict(new { message = error });

        return CreatedAtAction(nameof(GetById), new { id = device!.Id }, device);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DeviceWriteDto dto)
    {
        var (device, error) = await _service.UpdateAsync(id, dto);

        if (error is not null)
            return Conflict(new { message = error });

        return device is null ? NotFound() : Ok(device);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
