using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.hotelInsurances;
using Hotel_Booking_Domain.Core.Entitys;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class hotelInsurancesRepository : IhotelInsurancesRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<hotelInsurancesRepository> _logger;

		public hotelInsurancesRepository(AppDbContext appDbContext, ILogger<hotelInsurancesRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}

		public async Task<hotelInsurancesCreateDto> CreateAllhotelInsurances(hotelInsurancesCreateDto hotelInsurancesCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new hotelInsurances
				{
					FireAndNaturalDisasterInsurance = hotelInsurancesCreateDto.FireAndNaturalDisasterInsurance,
					PropertyAndEquipmentInsurance = hotelInsurancesCreateDto.PropertyAndEquipmentInsurance,
					EmployeeSupplementaryHealthInsurance = hotelInsurancesCreateDto.EmployeeSupplementaryHealthInsurance,
					GuestAccidentInsurance = hotelInsurancesCreateDto.GuestAccidentInsurance,
					
				};

				_appDbContext.hotelInsurances.Add(entity);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				hotelInsurancesCreateDto.Id = entity.Id;
				return hotelInsurancesCreateDto;
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while creating hotel insurance");
				return null;
			}
		}

		public async Task<bool> DeletehotelInsurances(int hotelInsurancesId, CancellationToken cancellationToken)
		{
			try
			{
				var entity = await _appDbContext.hotelInsurances
					.FirstOrDefaultAsync(h => h.Id == hotelInsurancesId, cancellationToken);

				if (entity == null)
				{
					_logger.LogWarning("Hotel insurance with Id {Id} not found", hotelInsurancesId);
					return false;
				}

				_appDbContext.hotelInsurances.Remove(entity);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while deleting hotel insurance with Id {Id}", hotelInsurancesId);
				return false;
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurances(CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelInsurances
					.Include(h => h.hotel)
					.Select(h => new hotelInsurancesCreateDto
					{
						Id = h.Id,
						FireAndNaturalDisasterInsurance = h.FireAndNaturalDisasterInsurance,
						PropertyAndEquipmentInsurance = h.PropertyAndEquipmentInsurance,
						EmployeeSupplementaryHealthInsurance = h.EmployeeSupplementaryHealthInsurance,
						GuestAccidentInsurance = h.GuestAccidentInsurance,
						HotelId = h.hotel != null ? h.hotel.Id : 0
					})
					.ToListAsync(cancellationToken);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while fetching all hotel insurances");
				return new List<hotelInsurancesCreateDto>();
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForAdminByHotelId(int HotelId, CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelInsurances
					.Where(h => h.hotel != null && h.hotel.Id == HotelId)
					.Select(h => new hotelInsurancesCreateDto
					{
						Id = h.Id,
						FireAndNaturalDisasterInsurance = h.FireAndNaturalDisasterInsurance,
						PropertyAndEquipmentInsurance = h.PropertyAndEquipmentInsurance,
						EmployeeSupplementaryHealthInsurance = h.EmployeeSupplementaryHealthInsurance,
						GuestAccidentInsurance = h.GuestAccidentInsurance,
						HotelId = HotelId
					})
					.ToListAsync(cancellationToken);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while fetching hotel insurances for hotelId {HotelId}", HotelId);
				return new List<hotelInsurancesCreateDto>();
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForUser(int UserId, CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelInsurances
					.Where(h => h.Userid == UserId)
					.Select(h => new hotelInsurancesCreateDto
					{
						Id = h.Id,
						FireAndNaturalDisasterInsurance = h.FireAndNaturalDisasterInsurance,
						PropertyAndEquipmentInsurance = h.PropertyAndEquipmentInsurance,
						EmployeeSupplementaryHealthInsurance = h.EmployeeSupplementaryHealthInsurance,
						GuestAccidentInsurance = h.GuestAccidentInsurance,
						HotelId = h.hotel != null ? h.hotel.Id : 0
					})
					.ToListAsync(cancellationToken);
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while fetching hotel insurances for userId {UserId}", UserId);
				return new List<hotelInsurancesCreateDto>();
			}
		}

		public async Task<hotelInsurancesUpdateDto> UpdateAllhotelInsurances(hotelInsurancesUpdateDto hotelInsurancesUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var entity = await _appDbContext.hotelInsurances
					.FirstOrDefaultAsync(h => h.Id == hotelInsurancesUpdateDto.Id, cancellationToken);

				if (entity == null)
				{
					_logger.LogWarning("Hotel insurance with Id {Id} not found", hotelInsurancesUpdateDto.Id);
					return null;
				}

				entity.FireAndNaturalDisasterInsurance = hotelInsurancesUpdateDto.FireAndNaturalDisasterInsurance;
				entity.PropertyAndEquipmentInsurance = hotelInsurancesUpdateDto.PropertyAndEquipmentInsurance;
				entity.EmployeeSupplementaryHealthInsurance = hotelInsurancesUpdateDto.EmployeeSupplementaryHealthInsurance;
				entity.GuestAccidentInsurance = hotelInsurancesUpdateDto.GuestAccidentInsurance;

				_appDbContext.hotelInsurances.Update(entity);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return hotelInsurancesUpdateDto;
			}
			catch (System.Exception ex)
			{
				_logger.LogError(ex, "Error while updating hotel insurance with Id {Id}", hotelInsurancesUpdateDto.Id);
				return null;
			}
		}
	}
}
