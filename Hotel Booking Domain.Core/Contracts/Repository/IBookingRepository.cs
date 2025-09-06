using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface IBookingRepository
	{
		Task<IReadOnlyCollection<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Booking>> GetBookingsByCustomerIdAsync(int customerId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Booking>> GetBookingsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Booking>> GetBookingsByRoomIdAsync(int roomId, CancellationToken cancellationToken);

		Task<Booking?> GetBookingByIdAsync(int bookingId, CancellationToken cancellationToken);

		Task<Booking> CreateBookingAsync(BookingCreateDto bookingCreateDto, CancellationToken cancellationToken);

		Task<Booking> UpdateBookingAsync(BookingUpdateDto bookingUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteBookingAsync(int id, CancellationToken cancellationToken);
	}
}
