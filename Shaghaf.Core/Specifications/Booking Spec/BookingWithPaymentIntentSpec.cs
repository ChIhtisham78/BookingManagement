using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Specifications;

public class BookingWithPaymentIntentSpec : BaseSpecifications<Booking>
{
    public BookingWithPaymentIntentSpec(string paymentIntentId)
        : base(b => b.PaymentIntentId == paymentIntentId)
    {
        AddInclude(b => b.Orders); // Include the associated orders if any
    }
}
