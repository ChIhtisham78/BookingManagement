using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using AutoMapper;
using Shaghaf.Core.Dtos;
using Shaghaf.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shaghaf.Core.Specifications.Booking_Spec;
using Shaghaf.Core.Dtos.BookingDtos;
using Shaghaf.Core.Dtos.PaymentDtos;

namespace Shaghaf.Service.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;           
            _mapper = mapper;
            _paymentService = paymentService;
        }
        private decimal CalculateTotalAmount(decimal amount, decimal? discount)
        {
            // حساب المبلغ النهائي بعد الخصم
            decimal discountAmount = discount ?? 0m;
            return amount - discountAmount;
        }

        public async Task<BookingDto> CreateBookingAsync(BookingToCreateDto bookingToCreateDto)
        {
            var booking = _mapper.Map<Booking>(bookingToCreateDto);

            // حساب TotalAmount
            booking.TotalAmount = CalculateTotalAmount(bookingToCreateDto.Amount, bookingToCreateDto.Discount);

            // حفظ الحجز أولاً لضمان وجود معرف صالح
            await _unitOfWork.Repository<Booking>().AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            // إنشاء جلسة الدفع
            var paymentSession = await _paymentService.CreateBookingCheckoutSession(new PaymentDto
            {
                Amount = booking.TotalAmount, // استخدام TotalAmount بدلاً من Amount
                Currency = booking.Currency,
                SuccessUrl = "https://localhost:7095/success",
                CancelUrl = "https://localhost:7095/cancel",
                BookingId = booking.Id
            });

            booking.PaymentStatus = false;

            // حفظ الحجز مرة أخرى مع تحديث معلومات الدفع
            _unitOfWork.Repository<Booking>().Update(booking);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task UpdateBookingAsync(BookingDto bookingDto)
        {
            var booking = await _bookingRepository.FindUniqueBookingAsync(
                bookingDto.RoomId, bookingDto.StartDate, bookingDto.EndDate, bookingDto.CustomerName);

            if (booking == null)
            {
                throw new KeyNotFoundException("No booking found matching the specified criteria.");
            }

            // إعادة حساب TotalAmount أثناء التحديث
            booking.TotalAmount = CalculateTotalAmount(bookingDto.TotalAmount, bookingDto.Discount);

            booking = _mapper.Map(bookingDto, booking);

            _bookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<BookingDto?> GetBookingDetailsAsync(int bookingId)
        {
            var spec = new BookWithAdditionalItemsSpecs(bookingId);
            var booking = await _unitOfWork.Repository<Booking>().GetByIdWithSpecAsync(spec);
            return booking == null ? null : _mapper.Map<BookingDto>(booking);
        }

        public async Task<IReadOnlyList<BookingDto>> GetAllBookingDetailsAsync()
        {
            var spec = new BookWithAdditionalItemsSpecs();
            var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<BookingDto>>(bookings);
        }

        public async Task<bool> CheckBookingPaymentStatusAsync(int bookingId)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            if (booking == null) return false;

            var paymentStatus = await _paymentService.CheckPaymentStatusAsync(booking.PaymentIntentId);

            if (paymentStatus.PaymentStatus)
            {
                booking.PaymentStatus = true;
                booking.Status = BookingStatus.Confirmed;
                _bookingRepository.Update(booking);
                await _unitOfWork.CompleteAsync();
            }

            return booking.PaymentStatus;
        }

        public async Task<bool> UpdatePaymentIntentAsync(int id, string paymentIntentId)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            booking.PaymentIntentId = paymentIntentId;
            _bookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            _unitOfWork.Repository<Booking>().Delete(booking);
            await _unitOfWork.CompleteAsync();
        }

    }
}
