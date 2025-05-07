using Shaghaf.Core.Dtos.RoomDtos;
using Shaghaf.Core.Entities.RoomEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IRoomService
    {
        Task<RoomDto> CreateRoomAsync(RoomToCreateDto roomDto);
        Task UpdateRoomAsync(RoomDto roomDto);
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<IReadOnlyList<RoomDto>> GetAllRoomsAsync();
        Task DeleteRoomAsync(int roomId);  
    }
}
