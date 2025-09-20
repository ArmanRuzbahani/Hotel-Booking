using Hotel_Booking_Domain.Core.DTO.Repository.Facility;
using Hotel_Booking_Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IFacilityService
	{
		Task<IReadOnlyCollection<Facility>> GetAllFacilitiesAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Facility?>> GetFacilitiesByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<Facility> CreateFacilityAsync(FacilityCreateDto facilityCreateDto, CancellationToken cancellationToken);

		Task<Facility> UpdateFacilityAsync(FacilityUpdateDto facilityUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteFacilityAsync(int id, CancellationToken cancellationToken);
	}
}
