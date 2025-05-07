using Shaghaf.Core.Dtos.BookingDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(BookingToCreateDto bookingToCreateDto);
        Task UpdateBookingAsync(BookingDto bookingDto);
        Task<BookingDto?> GetBookingDetailsAsync(int bookingId);
        Task<IReadOnlyList<BookingDto>> GetAllBookingDetailsAsync();
        Task<bool> CheckBookingPaymentStatusAsync(int bookingId);
        Task<bool> UpdatePaymentIntentAsync(int id, string paymentIntentId);
        Task DeleteBookingAsync(int bookingId);
    }
}
