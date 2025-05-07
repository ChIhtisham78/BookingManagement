using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos.PhotoSessionDtos;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;

public class PhotoSessionController : BaseApiController
{
    private readonly IPhotoSessionService _photoSessionService;

    public PhotoSessionController(IPhotoSessionService photoSessionService)
    {
        _photoSessionService = photoSessionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePhotoSession([FromBody] PhotoSessionToCreateDto photoSessionDto)
    {
        if (photoSessionDto == null)
        {
            return BadRequest("Invalid data.");
        }

        var result = await _photoSessionService.CreatePhotoSessionAsync(photoSessionDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePhotoSession(int id, [FromBody] PhotoSessionDto photoSessionDto)
    {
        if (photoSessionDto == null)
        {
            return BadRequest("Invalid data.");
        }

        photoSessionDto.Id = id;
        await _photoSessionService.UpdatePhotoSessionAsync(photoSessionDto);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPhotoSessionById(int id)
    {
        var result = await _photoSessionService.GetPhotoSessionByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPhotoSessions()
    {
        var result = await _photoSessionService.GetAllPhotoSessionsAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhotoSession(int id)
    {
        var result = await _photoSessionService.DeletePhotoSessionAsync(id);
        if (!result)
        {
            return NotFound("Photo session not found.");
        }
        return NoContent();
    }
}
