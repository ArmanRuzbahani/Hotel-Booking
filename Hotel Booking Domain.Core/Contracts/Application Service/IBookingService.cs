using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IBookingService 
	{
		Task<IReadOnlyCollection<BookingReadAdminDto>> GetAllBookingsAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByCustomerIdAsync(int customerId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingsByRoomIdAsync(int roomId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<BookingReadByHotelDto?>> GetBookingByIdAsync(int bookingId, CancellationToken cancellationToken);

		Task<Booking> CreateBookingAsync(BookingCreateDto bookingCreateDto, CancellationToken cancellationToken);

		Task<Booking> UpdateBookingAsync(BookingUpdateDto bookingUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteBookingAsync(int id, CancellationToken cancellationToken);
	}
}
