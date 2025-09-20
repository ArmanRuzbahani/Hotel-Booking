using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelManager;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelManagerService : IHotelManagerService
	{
		private readonly IHotelManagerRepository _hotelManagerRepository;
		private readonly ILogger<HotelManagerService> logger;

		public HotelManagerService(IHotelManagerRepository hotelManagerRepository, ILogger<HotelManagerService> logger)
		{
			_hotelManagerRepository = hotelManagerRepository;
			this.logger = logger;
		}

		public Task<HotelManager> CreateHotelManagerAsync(HotelManagerCreateDto hotelManagerCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteHotelManagerAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelManager>> GetAllHotelManagersAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<HotelManager?> GetHotelManagerByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<HotelManager> UpdateHotelManagerAsync(HotelManagerUpdateDto hotelManagerUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
