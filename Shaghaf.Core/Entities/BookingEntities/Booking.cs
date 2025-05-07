using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.OrderEntities;
using Shaghaf.Core.Entities.RoomEntities;

namespace Shaghaf.Core.Entities.BookingEntities
{
    public class Booking : BaseEntity
    {
        // Default constructor
        public Booking() { }

        // Parameterized constructor
        public Booking(DateTime startDate, DateTime endDate, string customerName, int seatCount, decimal totalAmount,
                       string currency, BookingStatus status, decimal? discount, int roomId, Room room,
                       int? birthdayId, Birthday? birthday, int? photoSessionId, PhotoSession? photoSession,
                       ICollection<Order>? orders, string paymentIntentId, bool paymentStatus, string sessionId)
        {
            StartDate = startDate;
            EndDate = endDate;
            CustomerName = customerName;
            SeatCount = seatCount;
            TotalAmount = totalAmount;
            Currency = currency;
            Status = status;
            Discount = discount;
            RoomId = roomId;
            Room = room ?? throw new ArgumentNullException(nameof(room));
            BirthdayId = birthdayId;
            Birthday = birthday;
            PhotoSessionId = photoSessionId;
            PhotoSession = photoSession;
            Orders = orders;
            PaymentIntentId = paymentIntentId;
            PaymentStatus = paymentStatus;
            SessionId = sessionId;
        }

        public DateTime StartDate { get; set; } // Start date of the booking
        public DateTime EndDate { get; set; } // End date of the booking
        public string CustomerName { get; set; } = null!; // Name of the customer
        public int SeatCount { get; set; } // Number of seats booked
        public decimal TotalAmount { get; set; } // Final total amount for the booking after discount
        public decimal? Discount { get; set; } // Optional discount applied to the booking
        public string Currency { get; set; } = null!; // Currency of the amount

        public BookingStatus Status { get; set; } // Status of the booking
        public int RoomId { get; set; } // ID of the booked room
        public Room Room { get; set; } = null!; // Associated room

        public int? BirthdayId { get; set; } // ID of the associated birthday event (optional)
        public Birthday? Birthday { get; set; } // Associated birthday event (optional)

        public int? PhotoSessionId { get; set; } // ID of the associated photo session (optional)
        public PhotoSession? PhotoSession { get; set; } // Associated photo session (optional)

        public ICollection<Order>? Orders { get; set; } // Optional collection of associated orders
        public string PaymentIntentId { get; set; } = null!; // Payment Intent ID for Stripe integration
        public bool PaymentStatus { get; set; } // Payment status for polling
        public string SessionId { get; set; } = null!; // Session ID for Stripe integration
    }
}
