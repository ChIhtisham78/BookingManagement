using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Infrastructure.Data;

namespace Shaghaf.Infrastructure.Repositories.Implementation
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(StoreContext context) : base(context)
        {
        }

        public async Task<Booking?> FindUniqueBookingAsync(int roomId, DateTime startDate, DateTime endDate, string customerName)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b =>
                b.RoomId == roomId &&
                b.StartDate >= startDate &&
                b.StartDate <= endDate &&
                b.CustomerName == customerName);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int bookingId, bool paymentStatus)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.PaymentStatus = paymentStatus;
            booking.Status = paymentStatus ? BookingStatus.Confirmed : BookingStatus.Pending;
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
