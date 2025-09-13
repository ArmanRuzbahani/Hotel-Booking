using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
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
	public class HotelRepository : IHotelRepository
	{
		private readonly AppDbContext _appdbcontext;
		private readonly ILogger<HotelRepository> _logger;

		public HotelRepository(AppDbContext appdbcontext, ILogger<HotelRepository> logger)
		{
			_appdbcontext = appdbcontext;
			_logger = logger;
		}
		public async Task<Hotel> CreateHotelAsync(HotelCreateDto hotelCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var hotel = new Hotel
				{
					Name = hotelCreateDto.Name,
					Description = hotelCreateDto.Description,
					ShortDescription = hotelCreateDto.ShortDescription,
					OpenAt = hotelCreateDto.OpenAt,
					CloseAt = hotelCreateDto.CloseAt,
					Picture = hotelCreateDto.Picture,
					iranCityForHotel = hotelCreateDto.IranCityForHotel,
					Stars = hotelCreateDto.Stars ?? 0,
					HotelManagerId = hotelCreateDto.SHotelManagerId,
					
				};

				await _appdbcontext.hotels.AddAsync(hotel, cancellationToken);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel");
				throw;
			}
		}



		public async Task<bool> DeleteHotelAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var hotel = await _appdbcontext.hotels
					.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

				if (hotel == null)
					return false;

				_appdbcontext.hotels.Remove(hotel);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var hotels = await _appdbcontext.hotels
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotels");
				throw;
			}
		}


		public async Task<Hotel?> GetHotelByIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var hotel = await _appdbcontext.hotels
					.AsNoTracking()
					.FirstOrDefaultAsync(h => h.Id == hotelId, cancellationToken);

				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving hotel by ID");
				throw;
			}
		}

		public async Task<Hotel> UpdateHotelAsync(HotelUpdateDto hotelUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var hotel = await _appdbcontext.hotels
					.FirstOrDefaultAsync(h => h.Id == hotelUpdateDto.Id, cancellationToken);

				if (hotel == null)
					return null;

				hotel.Name = hotelUpdateDto.Name;
				hotel.Description = hotelUpdateDto.Description;
				hotel.ShortDescription = hotelUpdateDto.ShortDescription;
				hotel.OpenAt = hotelUpdateDto.OpenAt;
				hotel.CloseAt = hotelUpdateDto.CloseAt;
				hotel.Picture = hotelUpdateDto.Picture;
				hotel.iranCityForHotel = hotelUpdateDto.IranCityForHotel;
				hotel.Stars = hotelUpdateDto.Stars;
				

				_appdbcontext.hotels.Update(hotel);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel");
				throw;
			}
		}
	}
}
