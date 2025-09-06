using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface IHotelRepository
	{
		Task<IReadOnlyCollection<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken);

		Task<Hotel?> GetHotelByIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<Hotel> CreateHotelAsync(HotelCreateDto hotelCreateDto, CancellationToken cancellationToken);

		Task<Hotel> UpdateHotelAsync(HotelUpdateDto hotelUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteHotelAsync(int id, CancellationToken cancellationToken);
	}
}
