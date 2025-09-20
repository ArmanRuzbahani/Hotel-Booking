using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.HotelAddress;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelAddressService : IHotelAdressService
	{
		private readonly IHotelAddressRepository _hotelAddressRepository;
		private readonly ILogger<HotelAddressService> _logger;

		public HotelAddressService(IHotelAddressRepository hotelAddressRepository, ILogger<HotelAddressService> logger)
		{
			_hotelAddressRepository = hotelAddressRepository;
			_logger = logger;
		}

		public Task<HotelAddress> CreateHotelAddressAsync(HotelAddressCreateDto hotelAddressCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteHotelAddressAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelAddress>> GetAllHotelAddressesAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelAddress?>> GetHotelAddressesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<HotelAddress> UpdateHotelAddressAsync(HotelAddressUpdateDto hotelAddressUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
