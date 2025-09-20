using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelRules;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	internal class HotelRulesService : IHotelRulesService
	{
		private readonly IHotelRulesRepository _HotelRulesRepository;
		private readonly ILogger<HotelRulesService> _logger;

		public HotelRulesService(IHotelRulesRepository hotelRulesRepository, ILogger<HotelRulesService> logger)
		{
			_HotelRulesRepository = hotelRulesRepository;
			_logger = logger;
		}

		public Task<HotelRules> CreateHotelRulesAsync(HotelRulesCreateDto hotelRulesCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteHotelRulesAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelRules>> GetAllHotelRulesAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelRules>> GetHotelRulesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<HotelRules> UpdateHotelRulesAsync(HotelRulesUpdateDto hotelRulesUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
