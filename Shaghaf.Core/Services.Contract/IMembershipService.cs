using Shaghaf.Core.Dtos.MembershipDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IMembershipService
    {
        Task<MembershipDto> CreateMembershipAsync(MembershipToCreateDto membershipDto);
        Task UpdateMembershipAsync(MembershipDto membershipDto);
        Task<MembershipDto?> GetMembershipByIdAsync(int id);
        Task<IReadOnlyList<MembershipDto>> GetAllMembershipsAsync();
        Task<bool> DeleteMembershipAsync(int id);
    }
}
