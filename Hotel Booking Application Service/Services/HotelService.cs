using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelService : IHotelService
	{
		private readonly IHotelRepository _hotelRepository;
		private readonly ILogger<HotelService> _logger;

		public HotelService(IHotelRepository hotelRepository, ILogger<HotelService> logger)
		{
			_hotelRepository = hotelRepository;
			_logger = logger;
		}

		public async Task<Hotel> CreateHotelAsync(HotelCreateDto hotelCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelCreateDto == null)
				{
					_logger.LogError("ایجاد هتل ناموفق بود: داده ورودی خالی است");
					throw new ArgumentNullException(nameof(hotelCreateDto));
				}

				var hotel = await _hotelRepository.CreateHotelAsync(hotelCreateDto, cancellationToken);
				if (hotel == null)
				{
					_logger.LogError("ایجاد هتل ناموفق بود: خطا در ذخیره‌سازی");
					throw new InvalidOperationException("خطا در ایجاد هتل");
				}

				_logger.LogInformation("هتل با موفقیت ایجاد شد: {HotelName}", hotel.Name);
				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد هتل با نام: {HotelName}", hotelCreateDto.Name);
				throw;
			}
		}

		public async Task<bool> DeleteHotelAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogError("حذف هتل ناموفق بود: شناسه نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				var result = await _hotelRepository.DeleteHotelAsync(id, cancellationToken);
				if (!result)
				{
					_logger.LogWarning("هتل با شناسه {HotelId} یافت نشد", id);
					return false;
				}

				_logger.LogInformation("هتل با شناسه {HotelId} با موفقیت حذف شد", id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف هتل با شناسه: {HotelId}", id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var hotels = await _hotelRepository.GetAllHotelsAsync(cancellationToken);
				if (hotels == null || hotels.Count == 0)
				{
					_logger.LogWarning("هیچ هتلی یافت نشد");
					return new List<Hotel>();
				}

				_logger.LogInformation("تعداد {HotelCount} هتل با موفقیت بازیابی شد", hotels.Count);
				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی لیست هتل‌ها");
				throw;
			}
		}

		public async Task<Hotel?> GetHotelByIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogError("بازیابی هتل ناموفق بود: شناسه نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId, cancellationToken);
				if (hotel == null)
				{
					_logger.LogWarning("هتل با شناسه {HotelId} یافت نشد", hotelId);
					return null;
				}

				_logger.LogInformation("هتل با شناسه {HotelId} با موفقیت بازیابی شد", hotelId);
				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی هتل با شناسه: {HotelId}", hotelId);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAsync(string searchTerm, CancellationToken cancellationToken)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(searchTerm))
				{
					_logger.LogWarning("جستجوی هتل با عبارت خالی: تمام هتل‌ها بازیابی می‌شوند");
				}

				var hotels = await _hotelRepository.SearchHotelsAsync(searchTerm, cancellationToken);
				if (hotels == null || hotels.Count == 0)
				{
					_logger.LogWarning("هیچ هتلی برای عبارت جستجو {SearchTerm} یافت نشد", searchTerm);
					return new List<Hotel>();
				}

				_logger.LogInformation("تعداد {HotelCount} هتل برای عبارت جستجو {SearchTerm} یافت شد", hotels.Count, searchTerm);
				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در جستجوی هتل‌ها با عبارت: {SearchTerm}", searchTerm);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAdvancedAsync(string searchTerm, int? minStars = null, int? maxStars = null, int pageSize = 50, CancellationToken cancellationToken = default)
		{
			try
			{
				if (minStars.HasValue && minStars.Value < 0)
				{
					_logger.LogError("جستجوی پیشرفته ناموفق بود: حداقل ستاره نامعتبر است");
					throw new ArgumentException("حداقل ستاره نامعتبر است");
				}

				if (maxStars.HasValue && maxStars.Value < 0)
				{
					_logger.LogError("جستجوی پیشرفته ناموفق بود: حداکثر ستاره نامعتبر است");
					throw new ArgumentException("حداکثر ستاره نامعتبر است");
				}

				if (pageSize <= 0)
				{
					_logger.LogError("جستجوی پیشرفته ناموفق بود: اندازه صفحه نامعتبر است");
					throw new ArgumentException("اندازه صفحه نامعتبر است");
				}

				var hotels = await _hotelRepository.SearchHotelsAdvancedAsync(searchTerm, minStars, maxStars, pageSize, cancellationToken);
				if (hotels == null || hotels.Count == 0)
				{
					_logger.LogWarning("هیچ هتلی برای جستجوی پیشرفته با عبارت {SearchTerm} یافت نشد", searchTerm);
					return new List<Hotel>();
				}

				_logger.LogInformation("تعداد {HotelCount} هتل برای جستجوی پیشرفته با عبارت {SearchTerm} یافت شد", hotels.Count, searchTerm);
				return hotels;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در جستجوی پیشرفته هتل‌ها با عبارت: {SearchTerm}", searchTerm);
				throw;
			}
		}

		public async Task<Hotel> UpdateHotelAsync(HotelUpdateDto hotelUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelUpdateDto == null || hotelUpdateDto.Id <= 0)
				{
					_logger.LogError("به‌روزرسانی هتل ناموفق بود: داده ورودی یا شناسه نامعتبر است");
					throw new ArgumentException("داده ورودی یا شناسه نامعتبر است");
				}

				var hotel = await _hotelRepository.UpdateHotelAsync(hotelUpdateDto, cancellationToken);
				if (hotel == null)
				{
					_logger.LogWarning("هتل با شناسه {HotelId} برای به‌روزرسانی یافت نشد", hotelUpdateDto.Id);
					throw new InvalidOperationException("هتل یافت نشد");
				}

				_logger.LogInformation("هتل با شناسه {HotelId} با موفقیت به‌روزرسانی شد", hotelUpdateDto.Id);
				return hotel;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی هتل با شناسه: {HotelId}", hotelUpdateDto.Id);
				throw;
			}
		}
	}
}