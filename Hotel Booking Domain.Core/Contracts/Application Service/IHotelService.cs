using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IHotelService
	{
		Task<IReadOnlyCollection<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken);

		Task<Hotel?> GetHotelByIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<Hotel> CreateHotelAsync(HotelCreateDto hotelCreateDto, CancellationToken cancellationToken);

		Task<Hotel> UpdateHotelAsync(HotelUpdateDto hotelUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteHotelAsync(int id, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Hotel>> SearchHotelsAsync(string searchTerm, CancellationToken cancellationToken);
		Task<IReadOnlyCollection<Hotel>> SearchHotelsAdvancedAsync(
			string searchTerm,
			int? minStars = null,
			int? maxStars = null,
			int pageSize = 50,
			CancellationToken cancellationToken = default);
	}
}
