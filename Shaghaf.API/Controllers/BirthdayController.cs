using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos.BirthdayDtos;
using Shaghaf.Core.Services.Contract;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;

public class BirthdayController : BaseApiController
{
    private readonly IBirthdayService _birthdayService;

    public BirthdayController(IBirthdayService birthdayService)
    {
        _birthdayService = birthdayService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBirthday([FromBody] BirthdayToCreateDto birthdayToCreateDto)
    {
        var result = await _birthdayService.CreateBirthdayAsync(birthdayToCreateDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBirthday(int id, [FromBody] BirthdayDto birthdayDto)
    {
        birthdayDto.Id = id;
        await _birthdayService.UpdateBirthdayAsync(birthdayDto);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBirthdayById(int id)
    {
        var result = await _birthdayService.GetBirthdayByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBirthdays()
    {
        var result = await _birthdayService.GetAllBirthdaysAsync();
        return Ok(result);
    }

    [HttpDelete("{birthdayId}")]
    public async Task<IActionResult> DeleteBirthday(int birthdayId)
    {
        await _birthdayService.DeleteBirthdayAsync(birthdayId);
        return NoContent();
    }

}
