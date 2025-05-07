using Shaghaf.Core.Entities.BookingEntities;

namespace Shaghaf.Core.Repositories.Contract
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<Booking?> FindUniqueBookingAsync(int roomId, DateTime startDate, DateTime endDate, string customerName);
        Task<bool> UpdatePaymentStatusAsync(int bookingId, bool paymentStatus); // Update booking payment status
    }
}
