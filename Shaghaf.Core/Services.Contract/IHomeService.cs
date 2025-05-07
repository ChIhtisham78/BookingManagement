using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.RoomEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Dtos.BirthdayDtos;
using Shaghaf.Core.Dtos.PhotoSessionDtos;
using Shaghaf.Core.Dtos.RoomDtos;

namespace Shaghaf.Core.Services.Contract
{
    public interface IHomeService
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsAsync(); // To get all menu items
        Task<IEnumerable<RoomDto>> GetRoomsAsync(); // To get all rooms
        Task<IEnumerable<MembershipDto>> GetMembershipsAsync(); // To get all memberships
        Task<IEnumerable<BirthdayDto>> GetBirthdaysAsync(); // To get all birthdays
        Task<IEnumerable<PhotoSessionDto>> GetPhotoSessionsAsync(); // To get all photo sessions
    }
}
