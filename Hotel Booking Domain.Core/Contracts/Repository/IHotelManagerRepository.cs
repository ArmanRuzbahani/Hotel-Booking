using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface IHotelManagerRepository
	{
		Task<IReadOnlyCollection<HotelManager>> GetAllHotelManagersAsync(CancellationToken cancellationToken);

		Task<HotelManager?> GetHotelManagerByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<HotelManager> CreateHotelManagerAsync(HotelManagerCreateDto hotelManagerCreateDto, CancellationToken cancellationToken);

		Task<HotelManager> UpdateHotelManagerAsync(HotelManagerUpdateDto hotelManagerUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteHotelManagerAsync(int id, CancellationToken cancellationToken);

	}
}
