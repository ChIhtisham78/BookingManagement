using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Services.Contract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shaghaf.Core.Dtos.RoomDtos;
using Talabat.APIs.Controllers;


public class RoomController : BaseApiController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomToCreateDto roomDto)
    {
        var result = await _roomService.CreateRoomAsync(roomDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDto roomDto)
    {
        roomDto.Id = id;
        await _roomService.UpdateRoomAsync(roomDto);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var result = await _roomService.GetRoomByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await _roomService.GetAllRoomsAsync();
        return Ok(result);
    }
    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        await _roomService.DeleteRoomAsync(roomId);
        return NoContent();
    }

}
