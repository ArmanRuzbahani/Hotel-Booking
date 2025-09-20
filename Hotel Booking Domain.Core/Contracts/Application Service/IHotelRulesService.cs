using Hotel_Booking_Domain.Core.DTO.Repository.HotelRules;
using Hotel_Booking_Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IHotelRulesService
	{
		Task<IReadOnlyCollection<HotelRules>> GetAllHotelRulesAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<HotelRules>> GetHotelRulesByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<HotelRules> CreateHotelRulesAsync(HotelRulesCreateDto hotelRulesCreateDto, CancellationToken cancellationToken);

		Task<HotelRules> UpdateHotelRulesAsync(HotelRulesUpdateDto hotelRulesUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteHotelRulesAsync(int id, CancellationToken cancellationToken);
	}
}
