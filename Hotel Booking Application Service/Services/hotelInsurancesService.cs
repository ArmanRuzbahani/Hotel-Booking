using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.hotelInsurances;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class hotelInsurancesService : IhotelInsurancesService
	{
		private readonly IhotelInsurancesRepository _ihotelInsurancesRepository;
		private readonly ILogger<hotelInsurancesService> _logger;

		public hotelInsurancesService(IhotelInsurancesRepository ihotelInsurancesRepository, ILogger<hotelInsurancesService> logger)
		{
			_ihotelInsurancesRepository = ihotelInsurancesRepository;
			_logger = logger;
		}

		public async Task<hotelInsurancesCreateDto> CreateAllhotelInsurances(hotelInsurancesCreateDto hotelInsurancesCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelInsurancesCreateDto == null)
				{
					_logger.LogWarning("❌ داده ورودی بیمه هتل خالی است.");
					throw new ArgumentNullException("اطلاعات بیمه هتل وارد نشده است.");
				}

				var result = await _ihotelInsurancesRepository.CreateAllhotelInsurances(hotelInsurancesCreateDto, cancellationToken);

				if (result == null)
				{
					_logger.LogError("❌ ثبت بیمه هتل ناموفق بود.");
					throw new Exception("خطا در ثبت بیمه هتل.");
				}

				_logger.LogInformation("✅ بیمه هتل با موفقیت ثبت شد.");
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام ثبت بیمه هتل.");
				throw;
			}
		}

		public async Task<bool> DeletehotelInsurances(int hotelInsurancesId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelInsurancesId <= 0)
				{
					_logger.LogWarning("❌ شناسه بیمه هتل نامعتبر است.");
					throw new ArgumentException("شناسه معتبر وارد کنید.");
				}

				var success = await _ihotelInsurancesRepository.DeletehotelInsurances(hotelInsurancesId, cancellationToken);

				if (!success)
				{
					_logger.LogError("❌ حذف بیمه هتل با شناسه {Id} ناموفق بود.", hotelInsurancesId);
					throw new Exception("خطا در حذف بیمه هتل.");
				}

				_logger.LogInformation("✅ بیمه هتل با شناسه {Id} با موفقیت حذف شد.", hotelInsurancesId);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام حذف بیمه هتل.");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurances(CancellationToken cancellationToken)
		{
			try
			{
				var result = await _ihotelInsurancesRepository.GetAllhotelInsurances(cancellationToken);

				if (result == null || result.Count == 0)
				{
					_logger.LogWarning("⚠️ هیچ بیمه هتلی یافت نشد.");
					return Array.Empty<hotelInsurancesCreateDto>();
				}

				_logger.LogInformation("✅ لیست همه بیمه‌های هتل با موفقیت دریافت شد.");
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام دریافت لیست بیمه‌های هتل.");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForAdminByHotelId(int HotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (HotelId <= 0)
				{
					_logger.LogWarning("❌ شناسه هتل نامعتبر است.");
					throw new ArgumentException("شناسه هتل باید معتبر باشد.");
				}

				var result = await _ihotelInsurancesRepository.GetAllhotelInsurancesForAdminByHotelId(HotelId, cancellationToken);

				if (result == null || result.Count == 0)
				{
					_logger.LogWarning("⚠️ بیمه‌ای برای هتل با شناسه {Id} یافت نشد.", HotelId);
					return Array.Empty<hotelInsurancesCreateDto>();
				}

				_logger.LogInformation("✅ بیمه‌های هتل با شناسه {Id} با موفقیت دریافت شد.", HotelId);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام دریافت بیمه‌های هتل برای ادمین.");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<hotelInsurancesCreateDto>> GetAllhotelInsurancesForUser(int UserId, CancellationToken cancellationToken)
		{
			try
			{
				if (UserId <= 0)
				{
					_logger.LogWarning("❌ شناسه کاربر نامعتبر است.");
					throw new ArgumentException("شناسه کاربر باید معتبر باشد.");
				}

				var result = await _ihotelInsurancesRepository.GetAllhotelInsurancesForUser(UserId, cancellationToken);

				if (result == null || result.Count == 0)
				{
					_logger.LogWarning("⚠️ بیمه‌ای برای کاربر با شناسه {Id} یافت نشد.", UserId);
					return Array.Empty<hotelInsurancesCreateDto>();
				}

				_logger.LogInformation("✅ بیمه‌های کاربر با شناسه {Id} با موفقیت دریافت شد.", UserId);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام دریافت بیمه‌های کاربر.");
				throw;
			}
		}

		public async Task<hotelInsurancesUpdateDto> UpdateAllhotelInsurances(hotelInsurancesUpdateDto hotelInsurancesUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelInsurancesUpdateDto == null)
				{
					_logger.LogWarning("❌ داده ورودی برای بروزرسانی بیمه هتل خالی است.");
					throw new ArgumentNullException("اطلاعات بروزرسانی بیمه هتل وارد نشده است.");
				}

				var result = await _ihotelInsurancesRepository.UpdateAllhotelInsurances(hotelInsurancesUpdateDto, cancellationToken);

				if (result == null)
				{
					_logger.LogError("❌ بروزرسانی بیمه هتل با شناسه {Id} ناموفق بود.", hotelInsurancesUpdateDto.Id);
					throw new Exception("خطا در بروزرسانی بیمه هتل.");
				}

				_logger.LogInformation("✅ بیمه هتل با شناسه {Id} با موفقیت بروزرسانی شد.", hotelInsurancesUpdateDto.Id);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ خطا هنگام بروزرسانی بیمه هتل.");
				throw;
			}
		}
	}
}
