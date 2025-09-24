using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelManager;
using Microsoft.Extensions.Logging;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelManagerService : IHotelManagerService
	{
		private readonly IHotelManagerRepository _hotelManagerRepository;
		private readonly ILogger<HotelManagerService> _logger;

		public HotelManagerService(IHotelManagerRepository hotelManagerRepository, ILogger<HotelManagerService> logger)
		{
			_hotelManagerRepository = hotelManagerRepository;
			_logger = logger;
		}

		public async Task<HotelManager> CreateHotelManagerAsync(HotelManagerCreateDto hotelManagerCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelManagerCreateDto == null)
					throw new ArgumentNullException(nameof(hotelManagerCreateDto), "مدیر هتل نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(hotelManagerCreateDto.Name))
					throw new ArgumentException("نام مدیر هتل الزامی است.");

				if (string.IsNullOrWhiteSpace(hotelManagerCreateDto.LastName))
					throw new ArgumentException("نام خانوادگی مدیر هتل الزامی است.");

				if (string.IsNullOrWhiteSpace(hotelManagerCreateDto.Email) || !hotelManagerCreateDto.Email.Contains("@"))
					throw new ArgumentException("ایمیل مدیر هتل معتبر نیست.");

				return await _hotelManagerRepository.CreateHotelManagerAsync(hotelManagerCreateDto, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام ایجاد مدیر هتل");
				throw;
			}
		}

		public async Task<bool> DeleteHotelManagerAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه مدیر هتل معتبر نیست.");

				var result = await _hotelManagerRepository.DeleteHotelManagerAsync(id, cancellationToken);

				if (!result)
					throw new KeyNotFoundException("مدیر هتل مورد نظر یافت نشد.");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در هنگام حذف مدیر هتل با شناسه {id}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelManager>> GetAllHotelManagersAsync(CancellationToken cancellationToken)
		{
			try
			{
				var managers = await _hotelManagerRepository.GetAllHotelManagersAsync(cancellationToken);

				if (managers == null || managers.Count == 0)
					throw new KeyNotFoundException("هیچ مدیری برای هتل‌ها یافت نشد.");

				return managers;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام دریافت لیست مدیران هتل");
				throw;
			}
		}

		public async Task<HotelManager?> GetHotelManagerByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
					throw new ArgumentException("شناسه هتل معتبر نیست.");

				var manager = await _hotelManagerRepository.GetHotelManagerByHotelIdAsync(hotelId, cancellationToken);

				if (manager == null)
					throw new KeyNotFoundException("برای این هتل هیچ مدیری یافت نشد.");

				return manager;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در هنگام دریافت مدیر هتل با شناسه {hotelId}");
				throw;
			}
		}

		public async Task<HotelManager> UpdateHotelManagerAsync(HotelManagerUpdateDto hotelManagerUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelManagerUpdateDto == null)
					throw new ArgumentNullException(nameof(hotelManagerUpdateDto), "اطلاعات مدیر هتل نمی‌تواند خالی باشد.");

				if (hotelManagerUpdateDto.Id <= 0)
					throw new ArgumentException("شناسه مدیر هتل معتبر نیست.");

				if (string.IsNullOrWhiteSpace(hotelManagerUpdateDto.Name))
					throw new ArgumentException("نام مدیر هتل الزامی است.");

				if (string.IsNullOrWhiteSpace(hotelManagerUpdateDto.LastName))
					throw new ArgumentException("نام خانوادگی مدیر هتل الزامی است.");

				var updatedManager = await _hotelManagerRepository.UpdateHotelManagerAsync(hotelManagerUpdateDto, cancellationToken);

				if (updatedManager == null)
					throw new KeyNotFoundException("مدیر هتل مورد نظر برای بروزرسانی یافت نشد.");

				return updatedManager;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام بروزرسانی مدیر هتل");
				throw;
			}
		}
	}
}
