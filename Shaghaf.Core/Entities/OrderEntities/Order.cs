using Shaghaf.Core.Entities.BookingEntities;

namespace Shaghaf.Core.Entities.OrderEntities
{
    public class Order : BaseEntity
    {
        // Default constructor
        public Order() { }

        // Parameterized constructor
        public Order(string phone, ICollection<OrderItem> orderItems,
                     string paymentIntentId, string sessionId, decimal total, string currency, int? bookingId,
                     Booking? booking, bool paymentStatus, string cartId)
        {
            Phone = phone;
            OrderItems = orderItems ?? new HashSet<OrderItem>();
            PaymentIntentId = paymentIntentId;
            SessionId = sessionId;
            Total = total;
            Currency = currency;
            BookingId = bookingId;
            Booking = booking;
            PaymentStatus = paymentStatus;
            CartId = cartId;
        }

        public string Phone { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public string PaymentIntentId { get; set; }
        public string SessionId { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }

        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
        public bool PaymentStatus { get; set; }

        public string CartId { get; set; } // Identifier for the associated cart
    }
}
