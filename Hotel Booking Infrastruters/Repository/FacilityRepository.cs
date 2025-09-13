using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Facility;
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
	public class FacilityRepository : IFacilityRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<FacilityRepository> _logger;

		public FacilityRepository(AppDbContext appDbContext, ILogger<FacilityRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}
		public async Task<Facility> CreateFacilityAsync(FacilityCreateDto facilityCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var facility = new Facility
				{
					Name = facilityCreateDto.Name
				};

				await _appDbContext.Facility.AddAsync(facility, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return facility;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating facility");
				throw;
			}
		}


		public async Task<bool> DeleteFacilityAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var facilityForDelete = await _appDbContext.Facility
					.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

				if (facilityForDelete == null)
				{
					return false;
				}

				_appDbContext.Facility.Remove(facilityForDelete);
				await _appDbContext.SaveChangesAsync(cancellationToken);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting facility");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<Facility>> GetAllFacilitiesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var facilities = await _appDbContext.Facility
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return facilities;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all facilities");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<Facility?>> GetFacilitiesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _appDbContext.Facility
					.Where(h => h.Id == hotelId)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving facilities by hotel ID");
				throw;
			}
		}



		public async Task<Facility> UpdateFacilityAsync(FacilityUpdateDto facilityUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var facility = await _appDbContext.Facility
					.FirstOrDefaultAsync(f => f.Id == facilityUpdateDto.Id, cancellationToken);

				if (facility == null)
				{
					return null;
				}

				facility.Name = facilityUpdateDto.Name;

				_appDbContext.Facility.Update(facility);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return facility;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating facility");
				throw;
			}
		}

	}
}
