using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.HotelAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IHotelAdressService
	{
		Task<IReadOnlyCollection<HotelAddress>> GetAllHotelAddressesAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<HotelAddress?>> GetHotelAddressesByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<HotelAddress> CreateHotelAddressAsync(HotelAddressCreateDto hotelAddressCreateDto, CancellationToken cancellationToken);

		Task<HotelAddress> UpdateHotelAddressAsync(HotelAddressUpdateDto hotelAddressUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteHotelAddressAsync(int id, CancellationToken cancellationToken);
	}
}
