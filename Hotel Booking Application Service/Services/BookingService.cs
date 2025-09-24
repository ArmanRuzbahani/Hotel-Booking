using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly ILogger<BookingService> _logger;

		public BookingService(IBookingRepository bookingRepository, ILogger<BookingService> logger)
		{
			if (bookingRepository == null)
			{
				throw new ArgumentNullException(nameof(bookingRepository), "ریپازیتوری رزرو نمی‌تواند خالی باشد.");
			}
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger), "لاگر نمی‌تواند خالی باشد.");
			}

			_bookingRepository = bookingRepository;
			_logger = logger;
		}

		public async Task<Booking> CreateBookingAsync(BookingCreateDto bookingCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (bookingCreateDto == null)
				{
					_logger.LogWarning("تلاش برای ایجاد رزرو با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(bookingCreateDto), "داده‌های ورودی رزرو نمی‌تواند خالی باشد.");
				}

				if (bookingCreateDto.CheckInDate >= bookingCreateDto.CheckOutDate)
				{
					_logger.LogWarning("تاریخ‌های رزرو نامعتبر: تاریخ ورود {CheckInDate} باید قبل از تاریخ خروج {CheckOutDate} باشد.",
						bookingCreateDto.CheckInDate, bookingCreateDto.CheckOutDate);
					throw new ArgumentException("تاریخ ورود باید قبل از تاریخ خروج باشد.");
				}

				if (bookingCreateDto.NumberOfGuests <= 0)
				{
					_logger.LogWarning("تعداد مهمانان نامعتبر: {NumberOfGuests}. باید بیشتر از صفر باشد.",
						bookingCreateDto.NumberOfGuests);
					throw new ArgumentException("تعداد مهمانان باید بیشتر از صفر باشد.");
				}

				var booking = await _bookingRepository.CreateBookingAsync(bookingCreateDto, cancellationToken);
				_logger.LogInformation("رزرو با شناسه {BookingId} برای مشتری با شناسه {CustomerId} با موفقیت ایجاد شد.",
					booking.Id, booking.CustomerId);
				return booking;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد رزرو برای مشتری با شناسه {CustomerId}.", bookingCreateDto.CustomerId);
				throw new InvalidOperationException("خطایی در هنگام ایجاد رزرو رخ داد.", ex);
			}
		}

		public async Task<bool> DeleteBookingAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogWarning("شناسه رزرو نامعتبر: {BookingId}. باید بیشتر از صفر باشد.", id);
					throw new ArgumentException("شناسه رزرو باید بیشتر از صفر باشد.");
				}

				var result = await _bookingRepository.DeleteBookingAsync(id, cancellationToken);
				if (result)
				{
					_logger.LogInformation("رزرو با شناسه {BookingId} با موفقیت حذف شد.", id);
				}
				else
				{
					_logger.LogWarning("رزرو با شناسه {BookingId} برای حذف یافت نشد.", id);
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف رزرو با شناسه {BookingId}.", id);
				throw new InvalidOperationException("خطایی در هنگام حذف رزرو رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadAdminDto>> GetAllBookingsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);
				if (bookings.Count > 0)
				{
					_logger.LogInformation("{Count} رزرو با موفقیت دریافت شد.", bookings.Count);
				}
				else
				{
					_logger.LogInformation("هیچ رزروی یافت نشد.");
				}
				return bookings;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت همه رزروها.");
				throw new InvalidOperationException("خطایی در هنگام دریافت رزروها رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingByIdAsync(int bookingId, CancellationToken cancellationToken)
		{
			try
			{
				if (bookingId <= 0)
				{
					_logger.LogWarning("شناسه رزرو نامعتبر: {BookingId}. باید بیشتر از صفر باشد.", bookingId);
					throw new ArgumentException("شناسه رزرو باید بیشتر از صفر باشد.");
				}

				var booking = await _bookingRepository.GetBookingByIdAsync(bookingId, cancellationToken);
				if (booking.Count == 0)
				{
					_logger.LogWarning("رزرو با شناسه {BookingId} یافت نشد.", bookingId);
				}
				else
				{
					_logger.LogInformation("رزرو با شناسه {BookingId} با موفقیت دریافت شد.", bookingId);
				}
				return booking;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت رزرو با شناسه {BookingId}.", bookingId);
				throw new InvalidOperationException("خطایی در هنگام دریافت رزرو رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				if (customerId <= 0)
				{
					_logger.LogWarning("شناسه مشتری نامعتبر: {CustomerId}. باید بیشتر از صفر باشد.", customerId);
					throw new ArgumentException("شناسه مشتری باید بیشتر از صفر باشد.");
				}

				var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId, cancellationToken);
				if (bookings.Count > 0)
				{
					_logger.LogInformation("{Count} رزرو برای مشتری با شناسه {CustomerId} با موفقیت دریافت شد.",
						bookings.Count, customerId);
				}
				else
				{
					_logger.LogInformation("هیچ رزروی برای مشتری با شناسه {CustomerId} یافت نشد.", customerId);
				}
				return bookings;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت رزروها برای مشتری با شناسه {CustomerId}.", customerId);
				throw new InvalidOperationException("خطایی در هنگام دریافت رزروهای مشتری رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogWarning("شناسه هتل نامعتبر: {HotelId}. باید بیشتر از صفر باشد.", hotelId);
					throw new ArgumentException("شناسه هتل باید بیشتر از صفر باشد.");
				}

				var bookings = await _bookingRepository.GetBookingsByHotelIdAsync(hotelId, cancellationToken);
				if (bookings.Count > 0)
				{
					_logger.LogInformation("{Count} رزرو برای هتل با شناسه {HotelId} با موفقیت دریافت شد.",
						bookings.Count, hotelId);
				}
				else
				{
					_logger.LogInformation("هیچ رزروی برای هتل با شناسه {HotelId} یافت نشد.", hotelId);
				}
				return bookings;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت رزروها برای هتل با شناسه {HotelId}.", hotelId);
				throw new InvalidOperationException("خطایی در هنگام دریافت رزروهای هتل رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByRoomIdAsync(int roomId, CancellationToken cancellationToken)
		{
			try
			{
				if (roomId <= 0)
				{
					_logger.LogWarning("شناسه اتاق نامعتبر: {RoomId}. باید بیشتر از صفر باشد.", roomId);
					throw new ArgumentException("شناسه اتاق باید بیشتر از صفر باشد.");
				}

				var bookings = await _bookingRepository.GetBookingsByRoomIdAsync(roomId, cancellationToken);
				if (bookings.Count > 0)
				{
					_logger.LogInformation("{Count} رزرو برای اتاق با شناسه {RoomId} با موفقیت دریافت شد.",
						bookings.Count, roomId);
				}
				else
				{
					_logger.LogInformation("هیچ رزروی برای اتاق با شناسه {RoomId} یافت نشد.", roomId);
				}
				return bookings;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت رزروها برای اتاق با شناسه {RoomId}.", roomId);
				throw new InvalidOperationException("خطایی در هنگام دریافت رزروهای اتاق رخ داد.", ex);
			}
		}

		public async Task<Booking> UpdateBookingAsync(BookingUpdateDto bookingUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (bookingUpdateDto == null)
				{
					_logger.LogWarning("تلاش برای به‌روزرسانی رزرو با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(bookingUpdateDto), "داده‌های ورودی به‌روزرسانی رزرو نمی‌تواند خالی باشد.");
				}

				if (bookingUpdateDto.Id <= 0)
				{
					_logger.LogWarning("شناسه رزرو نامعتبر: {BookingId}. باید بیشتر از صفر باشد.", bookingUpdateDto.Id);
					throw new ArgumentException("شناسه رزرو باید بیشتر از صفر باشد.");
				}

				if (bookingUpdateDto.CheckInDate.HasValue && bookingUpdateDto.CheckOutDate.HasValue &&
					bookingUpdateDto.CheckInDate >= bookingUpdateDto.CheckOutDate)
				{
					_logger.LogWarning("تاریخ‌های رزرو نامعتبر: تاریخ ورود {CheckInDate} باید قبل از تاریخ خروج {CheckOutDate} باشد.",
						bookingUpdateDto.CheckInDate, bookingUpdateDto.CheckOutDate);
					throw new ArgumentException("تاریخ ورود باید قبل از تاریخ خروج باشد.");
				}

				if (bookingUpdateDto.NumberOfGuests.HasValue && bookingUpdateDto.NumberOfGuests <= 0)
				{
					_logger.LogWarning("تعداد مهمانان نامعتبر: {NumberOfGuests}. باید بیشتر از صفر باشد.",
						bookingUpdateDto.NumberOfGuests);
					throw new ArgumentException("تعداد مهمانان باید بیشتر از صفر باشد.");
				}

				var booking = await _bookingRepository.UpdateBookingAsync(bookingUpdateDto, cancellationToken);
				_logger.LogInformation("رزرو با شناسه {BookingId} با موفقیت به‌روزرسانی شد.", booking.Id);
				return booking;
			}
			catch (KeyNotFoundException ex)
			{
				_logger.LogWarning("رزرو با شناسه {BookingId} برای به‌روزرسانی یافت نشد.", bookingUpdateDto.Id);
				throw new InvalidOperationException($"رزرو با شناسه {bookingUpdateDto.Id} یافت نشد.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی رزرو با شناسه {BookingId}.", bookingUpdateDto.Id);
				throw new InvalidOperationException("خطایی در هنگام به‌روزرسانی رزرو رخ داد.", ex);
			}
		}
	}
}