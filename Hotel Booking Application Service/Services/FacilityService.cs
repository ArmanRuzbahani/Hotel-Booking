using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Facility;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class FacilityService : IFacilityService
	{
		private readonly IFacilityRepository _facilityRepository;
		private readonly ILogger<FacilityService> _logger;

		public FacilityService(IFacilityRepository facilityRepository, ILogger<FacilityService> logger)
		{
			_facilityRepository = facilityRepository;
			_logger = logger;
		}

		public Task<Facility> CreateFacilityAsync(FacilityCreateDto facilityCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteFacilityAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Facility>> GetAllFacilitiesAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Facility?>> GetFacilitiesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Facility> UpdateFacilityAsync(FacilityUpdateDto facilityUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
