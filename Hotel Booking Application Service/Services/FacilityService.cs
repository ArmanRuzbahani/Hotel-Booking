using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Facility;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class FacilityService : IFacilityService
	{
		private readonly IFacilityRepository _facilityRepository;
		private readonly ILogger<FacilityService> _logger;

		public FacilityService(IFacilityRepository facilityRepository, ILogger<FacilityService> logger)
		{
			if (facilityRepository == null)
			{
				_logger?.LogError("ریپازیتوری امکانات نمی‌تواند خالی باشد.");
				throw new ArgumentNullException(nameof(facilityRepository), "ریپازیتوری امکانات نمی‌تواند خالی باشد.");
			}
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger), "لاگر نمی‌تواند خالی باشد.");
			}

			_facilityRepository = facilityRepository;
			_logger = logger;
		}

		public async Task<Facility> CreateFacilityAsync(FacilityCreateDto facilityCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (facilityCreateDto == null)
				{
					_logger.LogWarning("تلاش برای ایجاد امکان با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(facilityCreateDto), "داده‌های ورودی امکان نمی‌تواند خالی باشد.");
				}

				if (string.IsNullOrWhiteSpace(facilityCreateDto.Name))
				{
					_logger.LogWarning("نام امکان نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("نام امکان نمی‌تواند خالی باشد.");
				}

				var facility = await _facilityRepository.CreateFacilityAsync(facilityCreateDto, cancellationToken);
				_logger.LogInformation("امکان با شناسه {FacilityId} و نام '{Name}' با موفقیت ایجاد شد.", facility.Id, facility.Name);
				return facility;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد امکان با نام '{Name}'.", facilityCreateDto?.Name ?? "نامشخص");
				throw new InvalidOperationException("خطایی در هنگام ایجاد امکان رخ داد.", ex);
			}
		}

		public async Task<bool> DeleteFacilityAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogWarning("شناسه امکان نامعتبر: {FacilityId}. باید بیشتر از صفر باشد.", id);
					throw new ArgumentException("شناسه امکان باید بیشتر از صفر باشد.");
				}

				var result = await _facilityRepository.DeleteFacilityAsync(id, cancellationToken);
				if (result)
				{
					_logger.LogInformation("امکان با شناسه {FacilityId} با موفقیت حذف شد.", id);
				}
				else
				{
					_logger.LogWarning("امکان با شناسه {FacilityId} برای حذف یافت نشد.", id);
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف امکان با شناسه {FacilityId}.", id);
				throw new InvalidOperationException("خطایی در هنگام حذف امکان رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<Facility>> GetAllFacilitiesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var facilities = await _facilityRepository.GetAllFacilitiesAsync(cancellationToken);
				if (facilities.Count > 0)
				{
					_logger.LogInformation("{Count} امکان با موفقیت دریافت شد.", facilities.Count);
				}
				else
				{
					_logger.LogInformation("هیچ امکانی یافت نشد.");
				}
				return facilities;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت همه امکانات.");
				throw new InvalidOperationException("خطایی در هنگام دریافت امکانات رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<Facility?>> GetFacilitiesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogWarning("شناسه هتل نامعتبر: {HotelId}. باید بیشتر از صفر باشد.", hotelId);
					throw new ArgumentException("شناسه هتل باید بیشتر از صفر باشد.");
				}

				var facilities = await _facilityRepository.GetFacilitiesByHotelIdAsync(hotelId, cancellationToken);
				if (facilities.Count > 0)
				{
					_logger.LogInformation("{Count} امکان برای هتل با شناسه {HotelId} با موفقیت دریافت شد.", facilities.Count, hotelId);
				}
				else
				{
					_logger.LogInformation("هیچ امکانی برای هتل با شناسه {HotelId} یافت نشد.", hotelId);
				}
				return facilities;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت امکانات برای هتل با شناسه {HotelId}.", hotelId);
				throw new InvalidOperationException("خطایی در هنگام دریافت امکانات هتل رخ داد.", ex);
			}
		}

		public async Task<Facility> UpdateFacilityAsync(FacilityUpdateDto facilityUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (facilityUpdateDto == null)
				{
					_logger.LogWarning("تلاش برای به‌روزرسانی امکان با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(facilityUpdateDto), "داده‌های ورودی به‌روزرسانی امکان نمی‌تواند خالی باشد.");
				}

				if (facilityUpdateDto.Id <= 0)
				{
					_logger.LogWarning("شناسه امکان نامعتبر: {FacilityId}. باید بیشتر از صفر باشد.", facilityUpdateDto.Id);
					throw new ArgumentException("شناسه امکان باید بیشتر از صفر باشد.");
				}

				if (string.IsNullOrWhiteSpace(facilityUpdateDto.Name))
				{
					_logger.LogWarning("نام امکان نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("نام امکان نمی‌تواند خالی باشد.");
				}

				var facility = await _facilityRepository.UpdateFacilityAsync(facilityUpdateDto, cancellationToken);
				if (facility == null)
				{
					_logger.LogWarning("امکان با شناسه {FacilityId} برای به‌روزرسانی یافت نشد.", facilityUpdateDto.Id);
					throw new KeyNotFoundException($"امکان با شناسه {facilityUpdateDto.Id} یافت نشد.");
				}

				_logger.LogInformation("امکان با شناسه {FacilityId} و نام '{Name}' با موفقیت به‌روزرسانی شد.", facility.Id, facility.Name);
				return facility;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی امکان با شناسه {FacilityId}.", facilityUpdateDto?.Id ?? 0);
				throw new InvalidOperationException("خطایی در هنگام به‌روزرسانی امکان رخ داد.", ex);
			}
		}
	}
}