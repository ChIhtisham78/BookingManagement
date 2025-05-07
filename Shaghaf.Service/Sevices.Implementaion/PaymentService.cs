using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shaghaf.API.Helpers;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core;
using Stripe.Checkout;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe.Issuing;
using Shaghaf.Core.Dtos.PaymentDtos;

public class PaymentService : IPaymentService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<StripeSettings> stripeSettings, ILogger<PaymentService> logger)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stripeSettings = stripeSettings.Value;
            _logger = logger;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public async Task<Session> CreateOrderCheckoutSession(PaymentDto paymentDto, string cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            if (cart == null)
            {
                throw new ArgumentException("Cart not found.");
            }

            var totalAmount = (long)(cart.Items.Sum(i => i.Price * i.Quantity) * 100);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cart.Items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = paymentDto.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name
                        },
                        UnitAmount = (long)(item.Price * 100),
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = paymentDto.SuccessUrl,
                CancelUrl = paymentDto.CancelUrl,
                Metadata = new Dictionary<string, string>
            {
                { "CartId", cartId }
            }
            };

            var service = new SessionService();
            Session session = null;

            try
            {
                session = await service.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                throw new Exception($"Failed to create Stripe session: {ex.Message}");
            }

            // Update the cart with the correct PaymentIntentId from Stripe
            cart.SessionId = session.Id;
            cart.PaymentIntentId = session.PaymentIntentId;
            await _cartRepository.UpdateCartAsync(cart);    

            return session;
        }

  
    public async Task<Session> CreateBookingCheckoutSession(PaymentDto paymentDto)
    {
        if (!paymentDto.BookingId.HasValue)
        {
            throw new InvalidOperationException("BookingId must have a value for creating a checkout session.");
        }

        var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(paymentDto.BookingId.Value);
        if (booking == null) throw new KeyNotFoundException("Booking not found");

        // Manually create a PaymentIntent
        var paymentIntentService = new PaymentIntentService();
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)(paymentDto.Amount * 100),
            Currency = paymentDto.Currency,
            Description = $"Booking payment for {booking.Id}",
        };
        var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

        // Create session with linked PaymentIntent
        var options = new SessionCreateOptions
        {
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                SetupFutureUsage = "off_session",
                Metadata = new Dictionary<string, string>
            {
                { "BookingId", booking.Id.ToString() }
            }
            },
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = paymentDto.Currency,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Booking Payment"
                    },
                    UnitAmount = (long)(paymentDto.Amount * 100),
                },
                Quantity = 1,
            }
        },
            Mode = "payment",
            SuccessUrl = paymentDto.SuccessUrl,
            CancelUrl = paymentDto.CancelUrl,
        };

        var service = new SessionService();
        Session session;
        try
        {
            session = await service.CreateAsync(options);
            _logger.LogInformation($"Session created: {session.Id}, PaymentIntentId: {paymentIntent.Id}");
        }
        catch (StripeException ex)
        {
            _logger.LogError($"Stripe exception: {ex.Message}");
            throw new Exception($"Failed to create Stripe session: {ex.Message}");
        }

        if (string.IsNullOrEmpty(paymentIntent.Id))
        {
            _logger.LogError("PaymentIntentId is null or empty after creating Stripe session.");
            throw new Exception("PaymentIntentId is null or empty after session creation.");
        }

        booking.SessionId = session.Id;
        booking.Status = BookingStatus.Pending;
        booking.PaymentIntentId = paymentIntent.Id; // Correctly link the PaymentIntentId here
        await _unitOfWork.CompleteAsync();

        return session;
    }



    public async Task<PaymentStatusDto> CheckPaymentStatusAsync(string paymentIntentId)
        {
            if (string.IsNullOrEmpty(paymentIntentId))
            {
                throw new ArgumentException("PaymentIntentId cannot be null or empty.", nameof(paymentIntentId));
            }

            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);

            return new PaymentStatusDto
            {
                PaymentIntentId = paymentIntentId,
                PaymentStatus = paymentIntent.Status == "succeeded"
            };
        }
        
    }
