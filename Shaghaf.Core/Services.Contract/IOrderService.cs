using Shaghaf.Core.Dtos;
using Shaghaf.Core.Dtos.OrderDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string phone, string cartId, string sessionId = null, string paymentIntentId = null, int? bookingId = null);
        Task<IEnumerable<OrderToReturnDto>> GetOrdersByPhoneAsync(string phone);
        Task<OrderToReturnDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string phone);
        Task<bool> CheckOrderPaymentStatusAsync(int orderId);
        Task<bool> UpdatePaymentIntentAsync(int orderId, string paymentIntentId);
        Task<string> GetCartIdByOrderIdAsync(int orderId); // Add this method signature
        Task DeleteOrderAsync(int orderId);  // إضافة طريقة الحذف


    }
}
