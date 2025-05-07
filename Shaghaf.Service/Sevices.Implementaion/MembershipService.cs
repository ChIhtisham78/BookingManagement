using AutoMapper;
using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core;
using Microsoft.Extensions.Logging;
using Shaghaf.Service.Services.Implementation;
using Shaghaf.Core.Dtos.MembershipDtos;

public class MembershipService : IMembershipService
{
    private readonly IGenericRepository<Membership> _membershipRepository;
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MembershipService  > _logger;
    public MembershipService(IGenericRepository<Membership> membershipRepository, IGenericRepository<Room> roomRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<MembershipService> logger)
    {
        _membershipRepository = membershipRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;

    }
    public async Task<MembershipDto> CreateMembershipAsync(MembershipToCreateDto membershipDto)
    {
        var membership = _mapper.Map<Membership>(membershipDto);

        if (membershipDto.RoomIds != null && membershipDto.RoomIds.Any())
        {
            var rooms = await _roomRepository.GetAllWithSpecAsync(new RoomsByIdsSpec(membershipDto.RoomIds.ToList()));
            membership.Rooms = rooms.ToList();
        }


        await _membershipRepository.AddAsync(membership);
        await _unitOfWork.CompleteAsync(); // Save changes

        return _mapper.Map<MembershipDto>(membership); // This should now include RoomIds
    }

    public async Task UpdateMembershipAsync(MembershipDto membershipDto)
    {
        var membership = await _membershipRepository.GetByIdAsync(membershipDto.Id);

        if (membership == null)
        {
            throw new KeyNotFoundException("No membership found matching the specified criteria.");
        }

        _mapper.Map(membershipDto, membership);

        if (membershipDto.RoomIds != null && membershipDto.RoomIds.Any())
        {
            var rooms = await _roomRepository.GetAllWithSpecAsync(new RoomsByIdsSpec(membershipDto.RoomIds.ToList()));
            membership.Rooms = rooms.ToList(); // Convert IReadOnlyList<Room> to List<Room>
        }

        _membershipRepository.Update(membership);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<MembershipDto?> GetMembershipByIdAsync(int id)
    {
        var spec = new MembershipWithRoomsSpec(id);
        var membership = await _membershipRepository.GetEntityWithSpecAsync(spec);
        return membership == null ? null : _mapper.Map<MembershipDto>(membership);
    }

    public async Task<IReadOnlyList<MembershipDto>> GetAllMembershipsAsync()
    {
        var spec = new MembershipWithRoomsSpec();
        var memberships = await _membershipRepository.GetAllWithSpecAsync(spec);
        return _mapper.Map<IReadOnlyList<MembershipDto>>(memberships);
    }

    public async Task<bool> DeleteMembershipAsync(int id)
    {
        var membership = await _membershipRepository.GetByIdAsync(id);
        if (membership == null)
        {
            return false;
        }

        _membershipRepository.Delete(membership);
        await _unitOfWork.CompleteAsync(); // Don't forget to save changes!
        return true;
    }
}
