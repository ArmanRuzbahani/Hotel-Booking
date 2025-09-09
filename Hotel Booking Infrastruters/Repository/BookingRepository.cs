using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class BookingRepository : IBookingRepository
	{
		private readonly AppDbContext _appdbcontext;
		private readonly ILogger<BookingRepository> _logger;

		public BookingRepository(AppDbContext appdbcontext, ILogger<BookingRepository> logger)
		{
			_appdbcontext = appdbcontext ?? throw new ArgumentNullException(nameof(appdbcontext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<Booking> CreateBookingAsync(BookingCreateDto bookingCreateDto, CancellationToken cancellationToken)
		{
			if (bookingCreateDto == null)
				throw new ArgumentNullException(nameof(bookingCreateDto));

			var booking = new Booking
			{
				HotelId = bookingCreateDto.HotelId,
				RoomId = bookingCreateDto.RoomId,
				CustomerId = bookingCreateDto.CustomerId,
				CheckInDate = bookingCreateDto.CheckInDate,
				CheckOutDate = bookingCreateDto.CheckOutDate,
				NumberOfGuests = bookingCreateDto.NumberOfGuests,
				TotalPrice = bookingCreateDto.TotalPrice,
				NumberOfNights = bookingCreateDto.NumberOfNights,
				CreatedDate = DateTime.UtcNow
			};

			try
			{
				await _appdbcontext.bookings.AddAsync(booking, cancellationToken);
				await _appdbcontext.SaveChangesAsync(cancellationToken);
				_logger.LogInformation("Booking {BookingId} created successfully for CustomerId {CustomerId}", booking.Id, booking.CustomerId);
				return booking;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while creating booking for CustomerId {CustomerId}", booking.CustomerId);
				throw new InvalidOperationException("Database error occurred while creating the booking.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Booking creation was cancelled for CustomerId {CustomerId}", booking.CustomerId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while creating booking for CustomerId {CustomerId}", booking.CustomerId);
				throw new InvalidOperationException("Unexpected error occurred while creating the booking.", ex);
			}
		}

		public async Task<bool> DeleteBookingAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var booking = await _appdbcontext.bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
				if (booking == null)
				{
					_logger.LogWarning("Delete failed: Booking with ID {BookingId} not found.", id);
					return false;
				}

				_appdbcontext.bookings.Remove(booking);
				await _appdbcontext.SaveChangesAsync(cancellationToken);
				_logger.LogInformation("Booking {BookingId} deleted successfully.", id);
				return true;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while deleting booking with ID {BookingId}", id);
				throw new InvalidOperationException("Database error occurred while deleting the booking.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Booking deletion was cancelled for ID {BookingId}", id);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while deleting booking with ID {BookingId}", id);
				throw new InvalidOperationException("Unexpected error occurred while deleting the booking.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadAdminDto>> GetAllBookingsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var bookings = await _appdbcontext.bookings
					.AsNoTracking()
					.Select(b => new BookingReadAdminDto
					{
						Id = b.Id,
						HotelId = b.HotelId,
						RoomId = b.RoomId,
						CustomerId = b.CustomerId,
						CheckInDate = b.CheckInDate,
						CheckOutDate = b.CheckOutDate,
						NumberOfGuests = b.NumberOfGuests,
						TotalPrice = b.TotalPrice,
						NumberOfNights = b.NumberOfNights,
						CreatedDate = b.CreatedDate
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} bookings.", bookings);
				return bookings.AsReadOnly();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching all bookings.");
				throw new InvalidOperationException("Database error occurred while fetching bookings.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching all bookings was cancelled.");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching all bookings.");
				throw new InvalidOperationException("Unexpected error occurred while fetching bookings.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				var bookings = await _appdbcontext.bookings
					.AsNoTracking()
					.Where(b => b.CustomerId == customerId)
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} bookings for CustomerId {CustomerId}", bookings.Count, customerId);

				
				var bookingDtos = bookings.Select(b => new BookingReadByHotelDto
				{
					Id = b.Id,
					HotelId = b.HotelId,
					RoomId = b.RoomId,
					CustomerId = b.CustomerId,
					CheckInDate = b.CheckInDate,
					CheckOutDate = b.CheckOutDate,
					NumberOfGuests = b.NumberOfGuests,
					TotalPrice = b.TotalPrice,
					NumberOfNights = b.NumberOfNights,
					CreatedDate = b.CreatedDate
				}).ToList().AsReadOnly();

				return bookingDtos;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching bookings for CustomerId {CustomerId}", customerId);
				throw new InvalidOperationException("Database error occurred while fetching bookings for the customer.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching bookings for CustomerId {CustomerId} was cancelled.", customerId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching bookings for CustomerId {CustomerId}", customerId);
				throw new InvalidOperationException("Unexpected error occurred while fetching bookings for the customer.", ex);
			}
		}


		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var bookings = await _appdbcontext.bookings
					.AsNoTracking()
					.Where(b => b.HotelId == hotelId)
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} bookings for HotelId {HotelId}", bookings.Count, hotelId);

				
				var bookingDtos = bookings.Select(b => new BookingReadByHotelDto
				{
					Id = b.Id,
					HotelId = b.HotelId,
					RoomId = b.RoomId,
					CustomerId = b.CustomerId,
					CheckInDate = b.CheckInDate,
					CheckOutDate = b.CheckOutDate,
					NumberOfGuests = b.NumberOfGuests,
					TotalPrice = b.TotalPrice,
					NumberOfNights = b.NumberOfNights,
					CreatedDate = b.CreatedDate
				}).ToList().AsReadOnly();

				return bookingDtos;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching bookings for HotelId {HotelId}", hotelId);
				throw new InvalidOperationException("Database error occurred while fetching bookings for the hotel.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching bookings for HotelId {HotelId} was cancelled.", hotelId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching bookings for HotelId {HotelId}", hotelId);
				throw new InvalidOperationException("Unexpected error occurred while fetching bookings for the hotel.", ex);
			}
		}


		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByRoomIdAsync(int roomId, CancellationToken cancellationToken)
		{
			try
			{
				var bookings = await _appdbcontext.bookings
					.AsNoTracking()
					.Where(b => b.RoomId == roomId)
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} bookings for RoomId {RoomId}", bookings.Count, roomId);

				
				var bookingDtos = bookings.Select(b => new BookingReadByHotelDto
				{
					Id = b.Id,
					HotelId = b.HotelId,
					RoomId = b.RoomId,
					CustomerId = b.CustomerId,
					CheckInDate = b.CheckInDate,
					CheckOutDate = b.CheckOutDate,
					NumberOfGuests = b.NumberOfGuests,
					TotalPrice = b.TotalPrice,
					NumberOfNights = b.NumberOfNights,
					CreatedDate = b.CreatedDate
				}).ToList().AsReadOnly();

				return bookingDtos;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching bookings for RoomId {RoomId}", roomId);
				throw new InvalidOperationException("Database error occurred while fetching bookings for the room.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching bookings for RoomId {RoomId} was cancelled.", roomId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching bookings for RoomId {RoomId}", roomId);
				throw new InvalidOperationException("Unexpected error occurred while fetching bookings for the room.", ex);
			}
		}


		public async Task<Booking> UpdateBookingAsync(BookingUpdateDto bookingUpdateDto, CancellationToken cancellationToken)
		{
			if (bookingUpdateDto == null)
				throw new ArgumentNullException(nameof(bookingUpdateDto));

			try
			{
				var booking = await _appdbcontext.bookings.FirstOrDefaultAsync(b => b.Id == bookingUpdateDto.Id, cancellationToken);
				if (booking == null)
				{
					_logger.LogWarning("Booking with ID {BookingId} not found for update.", bookingUpdateDto.Id);
					throw new KeyNotFoundException($"Booking with ID {bookingUpdateDto.Id} not found.");
				}

				booking.CheckInDate = bookingUpdateDto.CheckInDate ?? booking.CheckInDate;
				booking.CheckOutDate = bookingUpdateDto.CheckOutDate ?? booking.CheckOutDate;
				booking.NumberOfGuests = bookingUpdateDto.NumberOfGuests ?? booking.NumberOfGuests;
				booking.TotalPrice = bookingUpdateDto.TotalPrice ?? booking.TotalPrice;
				booking.NumberOfNights = bookingUpdateDto.NumberOfNights ?? booking.NumberOfNights;

				await _appdbcontext.SaveChangesAsync(cancellationToken);
				_logger.LogInformation("Booking with ID {BookingId} updated successfully.", booking.Id);

				return booking;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while updating booking with ID {BookingId}", bookingUpdateDto.Id);
				throw new InvalidOperationException("Database error occurred while updating the booking.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Update operation for BookingId {BookingId} was cancelled.", bookingUpdateDto.Id);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while updating booking with ID {BookingId}", bookingUpdateDto.Id);
				throw new InvalidOperationException("Unexpected error occurred while updating the booking.", ex);
			}
		}

		public async Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingByIdAsync(int bookingId, CancellationToken cancellationToken)
		{
			try
			{
				var booking = await _appdbcontext.bookings
					.AsNoTracking()
					.Where(b => b.Id == bookingId)
					.Select(b => new BookingReadByHotelDto
					{
						Id = b.Id,
						HotelId = b.HotelId,
						RoomId = b.RoomId,
						CustomerId = b.CustomerId,
						CheckInDate = b.CheckInDate,
						CheckOutDate = b.CheckOutDate,
						NumberOfGuests = b.NumberOfGuests,
						TotalPrice = b.TotalPrice,
						NumberOfNights = b.NumberOfNights,
						CreatedDate = b.CreatedDate
					})
					.FirstOrDefaultAsync(cancellationToken);

				if (booking == null)
				{
					_logger.LogWarning("Booking with ID {BookingId} not found.", bookingId);
					return Array.Empty<BookingReadByHotelDto?>();
				}

				_logger.LogInformation("Booking with ID {BookingId} retrieved successfully.", bookingId);
				return new List<BookingReadByHotelDto?> { booking }.AsReadOnly();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error fetching booking with ID {BookingId}", bookingId);
				throw new InvalidOperationException("Error occurred while fetching the booking.", ex);
			}
		}

	}
}
