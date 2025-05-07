using AutoMapper;
using Shaghaf.Core;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.RoomEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Dtos.BirthdayDtos;
using Shaghaf.Core.Dtos.PhotoSessionDtos;
using Shaghaf.Core.Dtos.RoomDtos;

namespace Shaghaf.Service.Services.Implementation
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
        {
            var menuItems = await _unitOfWork.Repository<MenuItem>().GetAllAsync();
            return menuItems;
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync()
        {
            var spec = new RoomsWithMembershipsSpec(); // Use specification if needed
            var rooms = await _unitOfWork.Repository<Room>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<MembershipDto>> GetMembershipsAsync()
        {
            var spec = new MembershipWithRoomsSpec(); // Ensure this specification includes related Rooms
            var memberships = await _unitOfWork.Repository<Membership>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<MembershipDto>>(memberships);
        }

        public async Task<IEnumerable<BirthdayDto>> GetBirthdaysAsync()
        {
            var spec = new BirthdayWithDetailsSpec();
            var birthdays = await _unitOfWork.Repository<Birthday>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<BirthdayDto>>(birthdays);
        }

        public async Task<IEnumerable<PhotoSessionDto>> GetPhotoSessionsAsync()
        {
            var photoSessions = await _unitOfWork.Repository<PhotoSession>().GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoSessionDto>>(photoSessions);
        }
    }
}
