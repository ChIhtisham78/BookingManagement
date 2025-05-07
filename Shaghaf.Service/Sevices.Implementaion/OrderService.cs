using AutoMapper;
using Microsoft.Extensions.Logging;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Dtos.OrderDtos;
using Shaghaf.Core.Dtos.PaymentDtos;
using Shaghaf.Core.Entities.OrderEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shaghaf.Service.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ICartRepository cartRepository, ILogger<OrderService> logger, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _logger = logger;
            _paymentService = paymentService;
        }

        public async Task<OrderToReturnDto> CreateOrderAsync(string phone, string cartId, string sessionId = null, string paymentIntentId = null, int? bookingId = null)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            if (cart == null)
            {
                _logger.LogError($"Cart not found for ID: {cartId}");
                throw new KeyNotFoundException("Cart not found");
            }

            var orderItems = cart.Items.Select(item => new OrderItem
            {
                MenuItem = new MenuItemOrdered(item.Id, item.Name, item.PictureUrl),
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();

            var total = orderItems.Sum(item => item.Quantity * item.Price);

            var session = await _paymentService.CreateOrderCheckoutSession(new PaymentDto
            {
                Amount = total,
                Currency = "USD",
                SuccessUrl = "https://localhost:7095/success",
                CancelUrl = "https://localhost:7095/cancel",
                BookingId = bookingId,
                CartId = cartId
            }, cartId);

            var order = new Order
            {
                Phone = phone,
                OrderItems = orderItems,
                PaymentIntentId = session.PaymentIntentId, // Correct property
                SessionId = session.Id,                   // Use session.Id instead of session.SessionId
                Total = total,
                Currency = "USD",
                BookingId = bookingId,
                PaymentStatus = false,
                CartId = cartId
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersByPhoneAsync(string phone)
        {
            var spec = new OrdersByPhoneSpecification(phone);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string phone)
        {
            var spec = new OrdersByPhoneSpecification(phone);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<bool> CheckOrderPaymentStatusAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null) return false;

            var paymentStatus = await _paymentService.CheckPaymentStatusAsync(order.PaymentIntentId);

            if (paymentStatus.PaymentStatus)
            {
                order.PaymentStatus = true;
                order.Status = OrderStatus.Confirmed;
                _unitOfWork.Repository<Order>().Update(order);
                await _unitOfWork.CompleteAsync();
            }

            return order.PaymentStatus;
        }

        public async Task<bool> UpdatePaymentIntentAsync(int orderId, string paymentIntentId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogError($"Order with ID {orderId} not found.");
                return false;
            }

            order.PaymentIntentId = paymentIntentId;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Updated PaymentIntentId for Order ID {orderId}.");
            return true;
        }

        public async Task<string> GetCartIdByOrderIdAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            return order.CartId;
        }
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            _unitOfWork.Repository<Order>().Delete(order);
            await _unitOfWork.CompleteAsync();
        }

    }
}
