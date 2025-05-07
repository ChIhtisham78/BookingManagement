using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Services.Contract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shaghaf.Core.Dtos.PaymentDtos;
using Talabat.APIs.Controllers;


public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, IPaymentService paymentService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _paymentService = paymentService;
        _logger = logger;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        var phone = User.FindFirstValue(ClaimTypes.MobilePhone);
        if (string.IsNullOrEmpty(phone))
            return BadRequest(new { error = "User phone number is required." });

        var result = await _orderService.CreateOrderAsync(phone, orderDto.CartId);

        if (result == null)
            return BadRequest(new { error = "Order creation failed." });

        var paymentSession = await _paymentService.CreateOrderCheckoutSession(
            new PaymentDto
            {
                Amount = result.Total,
                Currency = orderDto.Currency,
                SuccessUrl = "https://localhost:7095/success",
                CancelUrl = "https://localhost:7095/cancel"
            },
            orderDto.CartId
        );

        if (paymentSession == null || string.IsNullOrEmpty(paymentSession.PaymentIntentId))
        {
            _logger.LogError("Failed to create payment session or retrieve PaymentIntentId.");
            return BadRequest(new { Message = "Failed to create payment session" });
        }

        return Ok(new { Order = result, PaymentSession = paymentSession });
    }

    [HttpGet("user-orders")]
    public async Task<IActionResult> GetOrdersForUser()
    {
        var phone = User.FindFirstValue(ClaimTypes.MobilePhone);
        if (string.IsNullOrEmpty(phone))
        {
            return BadRequest("User phone number is required.");
        }

        var orders = await _orderService.GetOrdersForUserAsync(phone);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound(new { error = "Order not found." });
        }
        return Ok(order);
    }
    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
    {
        if (!paymentDto.OrderId.HasValue)
        {
            return BadRequest(new { error = "OrderId must have a value." });
        }

        if (paymentDto.Amount <= 0)
        {
            return BadRequest(new { error = "Amount must be greater than zero." });
        }

        if (string.IsNullOrEmpty(paymentDto.Currency))
        {
            return BadRequest(new { error = "Currency must be provided." });
        }

        // Retrieve the cartId associated with the OrderId
        string cartId = await _orderService.GetCartIdByOrderIdAsync(paymentDto.OrderId.Value);
        if (string.IsNullOrEmpty(cartId))
        {
            return BadRequest(new { error = "Cart ID could not be retrieved for the provided Order ID." });
        }

        // Call the correct CreateCheckoutSession method with the cartId
        var session = await _paymentService.CreateOrderCheckoutSession(paymentDto, cartId);

        return Ok(new
        {
            PaymentIntentId = session.PaymentIntentId,
            SessionId = session.Id,
            PaymentUrl = session.Url // Include the payment URL here
        });
    }

    [HttpGet("check-order-payment-status/{orderId}")]
    public async Task<IActionResult> CheckOrderPaymentStatus(int orderId)
    {
        var isPaymentConfirmed = await _orderService.CheckOrderPaymentStatusAsync(orderId);

        if (!isPaymentConfirmed)
        {
            return BadRequest(new { error = "Payment not confirmed or order not found." });
        }

        return Ok(new { success = "Payment confirmed for the order." });
    }

    [HttpPost("update-payment-intent")]
    public async Task<IActionResult> UpdatePaymentIntent([FromBody] UpdatePaymentIntentDto updatePaymentIntentDto)
    {
        try
        {
            var isSuccess = await _orderService.UpdatePaymentIntentAsync(updatePaymentIntentDto.Id, updatePaymentIntentDto.PaymentIntentId);

            if (!isSuccess)
            {
                return NotFound(new { error = "Order not found." });
            }

            return Ok(new { success = "PaymentIntentId updated successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update PaymentIntentId for order: {OrderId}", updatePaymentIntentDto.Id);
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        await _orderService.DeleteOrderAsync(orderId);
        return NoContent();
    }

}
