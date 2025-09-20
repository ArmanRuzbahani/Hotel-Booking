using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly ILogger<BookingService> _logger;

		public BookingService(IBookingRepository bookingRepository, ILogger<BookingService> logger)
		{
			_bookingRepository = bookingRepository;
			_logger = logger;
		}

		public Task<Booking> CreateBookingAsync(BookingCreateDto bookingCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteBookingAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<BookingReadAdminDto>> GetAllBookingsAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingByIdAsync(int bookingId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByRoomIdAsync(int roomId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Booking> UpdateBookingAsync(BookingUpdateDto bookingUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
