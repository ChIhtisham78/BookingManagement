namespace Shaghaf.Core.Dtos.PaymentDtos
{
    public class PaymentDto
    {
        public int? OrderId { get; set; }
        public int? BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string? CartId { get; set; } // Make CartId nullable
    }
}
