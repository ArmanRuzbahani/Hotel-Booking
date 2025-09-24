using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelRules;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	internal class HotelRulesService : IHotelRulesService
	{
		private readonly IHotelRulesRepository _hotelRulesRepository;
		private readonly ILogger<HotelRulesService> _logger;

		public HotelRulesService(IHotelRulesRepository hotelRulesRepository, ILogger<HotelRulesService> logger)
		{
			_hotelRulesRepository = hotelRulesRepository;
			_logger = logger;
		}

		public async Task<HotelRules> CreateHotelRulesAsync(HotelRulesCreateDto hotelRulesCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelRulesCreateDto == null)
				{
					_logger.LogError("ایجاد قانون هتل ناموفق بود: داده ورودی خالی است");
					throw new ArgumentNullException(nameof(hotelRulesCreateDto));
				}

				if (hotelRulesCreateDto.HotelId <= 0)
				{
					_logger.LogError("ایجاد قانون هتل ناموفق بود: شناسه هتل نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				var rule = await _hotelRulesRepository.CreateHotelRulesAsync(hotelRulesCreateDto, cancellationToken);
				if (rule == null)
				{
					_logger.LogError("ایجاد قانون هتل ناموفق بود: خطا در ذخیره‌سازی");
					throw new InvalidOperationException("خطا در ایجاد قانون هتل");
				}

				_logger.LogInformation("قانون هتل با نام {RuleName} با موفقیت ایجاد شد", rule.Name);
				return rule;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد قانون هتل با نام: {RuleName}", hotelRulesCreateDto.Name);
				throw;
			}
		}

		public async Task<bool> DeleteHotelRulesAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogError("حذف قانون هتل ناموفق بود: شناسه نامعتبر است");
					throw new ArgumentException("شناسه قانون نامعتبر است");
				}

				var result = await _hotelRulesRepository.DeleteHotelRulesAsync(id, cancellationToken);
				if (!result)
				{
					_logger.LogWarning("قانون هتل با شناسه {RuleId} یافت نشد", id);
					return false;
				}

				_logger.LogInformation("قانون هتل با شناسه {RuleId} با موفقیت حذف شد", id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف قانون هتل با شناسه: {RuleId}", id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelRules>> GetAllHotelRulesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var rules = await _hotelRulesRepository.GetAllHotelRulesAsync(cancellationToken);
				if (rules == null || rules.Count == 0)
				{
					_logger.LogWarning("هیچ قانون هتلی یافت نشد");
					return new List<HotelRules>();
				}

				_logger.LogInformation("تعداد {RuleCount} قانون هتل با موفقیت بازیابی شد", rules.Count);
				return rules;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی لیست قوانین هتل");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelRules>> GetHotelRulesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogError("بازیابی قوانین هتل ناموفق بود: شناسه هتل نامعتبر است");
					throw new ArgumentException("شناسه هتل نامعتبر است");
				}

				var rules = await _hotelRulesRepository.GetHotelRulesByHotelIdAsync(hotelId, cancellationToken);
				if (rules == null || rules.Count == 0)
				{
					_logger.LogWarning("هیچ قانون هتلی برای هتل با شناسه {HotelId} یافت نشد", hotelId);
					return new List<HotelRules>();
				}

				_logger.LogInformation("تعداد {RuleCount} قانون هتل برای هتل با شناسه {HotelId} بازیابی شد", rules.Count, hotelId);
				return rules;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در بازیابی قوانین هتل برای هتل با شناسه: {HotelId}", hotelId);
				throw;
			}
		}

		public async Task<HotelRules> UpdateHotelRulesAsync(HotelRulesUpdateDto hotelRulesUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelRulesUpdateDto == null || hotelRulesUpdateDto.Id <= 0)
				{
					_logger.LogError("به‌روزرسانی قانون هتل ناموفق بود: داده ورودی یا شناسه نامعتبر است");
					throw new ArgumentException("داده ورودی یا شناسه نامعتبر است");
				}

				var rule = await _hotelRulesRepository.UpdateHotelRulesAsync(hotelRulesUpdateDto, cancellationToken);
				if (rule == null)
				{
					_logger.LogWarning("قانون هتل با شناسه {RuleId} برای به‌روزرسانی یافت نشد", hotelRulesUpdateDto.Id);
					throw new InvalidOperationException("قانون هتل یافت نشد");
				}

				_logger.LogInformation("قانون هتل با شناسه {RuleId} با موفقیت به‌روزرسانی شد", hotelRulesUpdateDto.Id);
				return rule;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی قانون هتل با شناسه: {RuleId}", hotelRulesUpdateDto.Id);
				throw;
			}
		}
	}
}