using Shaghaf.Core.Dtos.BirthdayDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IBirthdayService
    {
        Task<BirthdayDto> CreateBirthdayAsync(BirthdayToCreateDto birthdayToCreateDto);
        Task UpdateBirthdayAsync(BirthdayDto birthdayDto);
        Task<BirthdayDto?> GetBirthdayByIdAsync(int id);
        Task<IReadOnlyList<BirthdayDto>> GetAllBirthdaysAsync();
        Task DeleteBirthdayAsync(int birthdayId);  // إضافة طريقة الحذف
    }
}
