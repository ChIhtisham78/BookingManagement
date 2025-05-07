using Shaghaf.Core.Dtos.PaymentDtos;
using Stripe.Checkout;
using System.Threading.Tasks;

public interface IPaymentService
{
    Task<Session> CreateBookingCheckoutSession(PaymentDto paymentDto); // For bookings
    Task<Session> CreateOrderCheckoutSession(PaymentDto paymentDto, string cartId); // For orders with a cart
    Task<PaymentStatusDto> CheckPaymentStatusAsync(string paymentIntentId); // Check payment status
}
