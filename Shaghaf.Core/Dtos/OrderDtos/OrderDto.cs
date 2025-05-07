using System.ComponentModel.DataAnnotations;

public class OrderDto
{
    public string CartId { get; set; } // Cart ID for associating the order with a cart
    public string Currency { get; set; } // Currency for the payment
    public int? BookingId { get; set; } // Optional Booking ID to associate the order with a booking
}   
