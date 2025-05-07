namespace Shaghaf.Core.Dtos.OrderDtos
{

    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string Phone { get; set; } // Customer's phone number
        public DateTimeOffset OrderDate { get; set; } // Order date with time zone
        public string Status { get; set; } // Status of the order
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>(); // List of items in the order
        public decimal Total { get; set; } // Total cost of the order
        public string PaymentIntentId { get; set; } // Payment Intent ID
        public bool PaymentStatus { get; set; } // Payment status for polling
    }


}
