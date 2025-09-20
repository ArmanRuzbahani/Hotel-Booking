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

		// متد Search جدید - شبیه GetAllHotelsAsync
		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAsync(string searchTerm, CancellationToken cancellationToken)
		{
			try
			{
				// اگر searchTerm خالی باشه، همه هتل‌ها رو برگردون
				if (string.IsNullOrWhiteSpace(searchTerm))
				{
					return await GetAllHotelsAsync(cancellationToken);
				}

				// تبدیل به حروف کوچک برای جستجوی بهتر
				var searchTermLower = searchTerm.ToLower().Trim();

				var hotels = await _appdbcontext.hotels
					.AsNoTracking()
					.Where(h =>
						(h.Name != null && h.Name.ToLower().Contains(searchTermLower)) ||
						(h.Description != null && h.Description.ToLower().Contains(searchTermLower)) ||
						(h.ShortDescription != null && h.ShortDescription.ToLower().Contains(searchTermLower)) ||
						h.iranCityForHotel.ToString().ToLower().Contains(searchTermLower))
					.OrderByDescending(h => h.Id)
					.ToListAsync(cancellationToken);

				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while searching hotels with term: {SearchTerm}", searchTerm);
				throw;
			}
		}

		
		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAdvancedAsync(
			string searchTerm,
			int? minStars = null,
			int? maxStars = null,
			int pageSize = 50,
			CancellationToken cancellationToken = default)
		{
			try
			{
				var query = _appdbcontext.hotels.AsNoTracking();

				
				if (!string.IsNullOrWhiteSpace(searchTerm))
				{
					var searchTermLower = searchTerm.ToLower().Trim();
					query = query.Where(h =>
						(h.Name != null && h.Name.ToLower().Contains(searchTermLower)) ||
						(h.Description != null && h.Description.ToLower().Contains(searchTermLower)) ||
						(h.ShortDescription != null && h.ShortDescription.ToLower().Contains(searchTermLower)) ||
						h.iranCityForHotel.ToString().ToLower().Contains(searchTermLower));
				}

				
				if (minStars.HasValue)
				{
					query = query.Where(h => h.Stars >= minStars.Value);
				}

				if (maxStars.HasValue)
				{
					query = query.Where(h => h.Stars <= maxStars.Value);
				}

				var hotels = await query
					.OrderByDescending(h => h.Stars) // بر اساس ستاره مرتب کن
					.ThenByDescending(h => h.Id) // سپس جدیدترین‌ها
					.Take(pageSize) // محدود کردن تعداد نتایج
					.ToListAsync(cancellationToken);

				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while advanced searching hotels with term: {SearchTerm}", searchTerm);
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