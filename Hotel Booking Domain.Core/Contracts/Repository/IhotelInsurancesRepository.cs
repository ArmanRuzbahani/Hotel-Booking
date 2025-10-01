using Hotel_Booking_Domain.Core.DTO.Repository.hotelInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public  interface IhotelInsurancesRepository
	{
		Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForUser(int UserId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForAdminByHotelId(int HotelId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurances(CancellationToken cancellationToken);

		Task<hotelInsurancesCreateDto> CreateAllhotelInsurances(hotelInsurancesCreateDto hotelInsurancesCreateDto, CancellationToken cancellationToken);

		Task<hotelInsurancesUpdateDto> UpdateAllhotelInsurances(hotelInsurancesUpdateDto hotelInsurancesUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeletehotelInsurances(int hotelInsurancesId, CancellationToken cancellationToken);


	}
}
