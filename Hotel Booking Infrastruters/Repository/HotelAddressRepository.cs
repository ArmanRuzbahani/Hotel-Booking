using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.HotelAddress;
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
	public class HotelAddressRepository : IHotelAddressRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<HotelAddressRepository> _logger;

		public HotelAddressRepository(AppDbContext appDbContext, ILogger<HotelAddressRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}
		public async Task<HotelAddress> CreateHotelAddressAsync(HotelAddressCreateDto hotelAddressCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var hotelAddress = new HotelAddress
				{
					HotelId = hotelAddressCreateDto.HotelId,
					Address = hotelAddressCreateDto.Address,
					AddressName = hotelAddressCreateDto.AddressName
				};

				await _appDbContext.hotelAddresses.AddAsync(hotelAddress, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return hotelAddress;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel address");
				throw;
			}
		}


		public async Task<bool> DeleteHotelAddressAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var hotelAddressForDel = await _appDbContext.hotelAddresses
					.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

				if (hotelAddressForDel == null)
					return false;

				_appDbContext.hotelAddresses.Remove(hotelAddressForDel);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel address");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<HotelAddress>> GetAllHotelAddressesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _appDbContext.hotelAddresses
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotel addresses");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<HotelAddress?>> GetHotelAddressesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _appDbContext.hotelAddresses
					.Where(a => a.HotelId == hotelId)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving hotel addresses by hotel ID");
				throw;
			}
		}


		public async Task<HotelAddress> UpdateHotelAddressAsync(HotelAddressUpdateDto hotelAddressUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var hotelAddress = await _appDbContext.hotelAddresses
					.FirstOrDefaultAsync(a => a.Id == hotelAddressUpdateDto.Id, cancellationToken);

				if (hotelAddress == null)
					return null;

				hotelAddress.Address = hotelAddressUpdateDto.Address;
				hotelAddress.AddressName = hotelAddressUpdateDto.AddressName;

				_appDbContext.hotelAddresses.Update(hotelAddress);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return hotelAddress;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel address");
				throw;
			}
		}

	}
}
