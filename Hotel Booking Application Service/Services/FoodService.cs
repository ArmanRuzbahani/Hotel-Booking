using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Food;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class FoodService : IFoodService
	{
		private readonly IFoodRepository _foodRepository;
		private readonly ILogger<FoodService> _logger;

		public FoodService(IFoodRepository foodRepository, ILogger<FoodService> logger)
		{
			_foodRepository = foodRepository ?? throw new ArgumentNullException(nameof(foodRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<Food> CreateFoodAsync(FoodCreateDto foodCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (foodCreateDto == null)
					throw new ArgumentNullException(nameof(foodCreateDto), "اطلاعات غذا نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(foodCreateDto.Name))
					throw new ArgumentException("نام غذا نمی‌تواند خالی باشد.");

				if (foodCreateDto.Name.Length < 2)
					throw new ArgumentException("نام غذا باید حداقل ۲ کاراکتر باشد.");

				var food = await _foodRepository.CreateFoodAsync(foodCreateDto, cancellationToken);

				_logger.LogInformation("غذا '{FoodName}' با موفقیت ایجاد شد (شناسه: {FoodId})", food.Name, food.Id);
				return food;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد غذا");
				throw;
			}
		}

		public async Task<Food> UpdateFoodAsync(FoodUpdateDto foodUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (foodUpdateDto == null)
					throw new ArgumentNullException(nameof(foodUpdateDto), "اطلاعات غذا برای بروزرسانی نمی‌تواند خالی باشد.");

				if (foodUpdateDto.Id <= 0)
					throw new ArgumentException("شناسه غذا معتبر نیست.");

				if (string.IsNullOrWhiteSpace(foodUpdateDto.Name))
					throw new ArgumentException("نام غذا نمی‌تواند خالی باشد.");

				var updated = await _foodRepository.UpdateFoodAsync(foodUpdateDto, cancellationToken);

				if (updated == null)
					throw new KeyNotFoundException($"غذای با شناسه {foodUpdateDto.Id} یافت نشد.");

				_logger.LogInformation("غذای '{FoodName}' با موفقیت بروزرسانی شد (شناسه: {FoodId})", updated.Name, updated.Id);
				return updated;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در بروزرسانی غذا با شناسه {foodUpdateDto.Id}");
				throw;
			}
		}

		public async Task<bool> DeleteFoodAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه غذا معتبر نیست.");

				var deleted = await _foodRepository.DeleteFoodAsync(id, cancellationToken);

				if (!deleted)
					throw new KeyNotFoundException($"غذای با شناسه {id} یافت نشد.");

				_logger.LogInformation("غذای با شناسه {FoodId} با موفقیت حذف شد.", id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در حذف غذا با شناسه {id}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Food>> GetAllFoodsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var foods = await _foodRepository.GetAllFoodsAsync(cancellationToken);

				if (foods == null || !foods.Any())
					throw new KeyNotFoundException("هیچ غذایی در سیستم یافت نشد.");

				_logger.LogInformation("تعداد {Count} غذا با موفقیت دریافت شد.", foods.Count);
				return foods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت لیست همه غذاها");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Food?>> GetFoodsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
					throw new ArgumentException("شناسه هتل معتبر نیست.");

				var foods = await _foodRepository.GetFoodsByHotelIdAsync(hotelId, cancellationToken);

				if (foods == null || !foods.Any())
					throw new KeyNotFoundException($"هیچ غذایی برای هتل با شناسه {hotelId} یافت نشد.");

				_logger.LogInformation("تعداد {Count} غذا برای هتل با شناسه {HotelId} دریافت شد.", foods.Count, hotelId);
				return foods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در دریافت غذاها برای هتل با شناسه {hotelId}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Food>> SearchFoodsAsync(string searchTerm, CancellationToken cancellationToken)
		{
			try
			{
				var foods = await _foodRepository.SearchFoodsAsync(searchTerm, cancellationToken);

				if (foods == null || !foods.Any())
					throw new KeyNotFoundException("هیچ غذایی با این شرایط یافت نشد.");

				_logger.LogInformation("تعداد {Count} نتیجه برای جستجوی غذا با عبارت '{SearchTerm}' یافت شد.", foods.Count(), searchTerm);
				return foods.ToList().AsReadOnly();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در جستجوی غذا با عبارت {SearchTerm}", searchTerm);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Food>> SearchFoodsAdvancedAsync(string searchTerm, int? hotelId = null, int pageSize = 50, CancellationToken cancellationToken = default)
		{
			try
			{
				var foods = await _foodRepository.SearchFoodsAdvancedAsync(searchTerm, hotelId, pageSize, cancellationToken);

				if (foods == null || !foods.Any())
					throw new KeyNotFoundException("هیچ غذایی با شرایط جستجوی پیشرفته یافت نشد.");

				_logger.LogInformation("تعداد {Count} نتیجه برای جستجوی پیشرفته غذا (هتل: {HotelId}, عبارت: {SearchTerm}) یافت شد.",
					foods.Count(), hotelId.HasValue ? hotelId.Value : 0, searchTerm);

				return foods.ToList().AsReadOnly();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در جستجوی پیشرفته غذا با عبارت {SearchTerm} و هتل {HotelId}", searchTerm, hotelId);
				throw;
			}
		}
	}
}
