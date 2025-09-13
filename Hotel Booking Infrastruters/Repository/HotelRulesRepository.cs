using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelRules;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.EntityFrameworkCore;


namespace Hotel_Booking_Infrastruters.Repository
{
	public class HotelRulesRepository : IHotelRulesRepository
	{
		private readonly AppDbContext _appdbcontext;
		private readonly ILogger<HotelRulesRepository> _logger;

		public HotelRulesRepository(AppDbContext appdbcontext, ILogger<HotelRulesRepository> logger)
		{
			_appdbcontext = appdbcontext;
			_logger = logger;
		}
		public async Task<HotelRules> CreateHotelRulesAsync(HotelRulesCreateDto hotelRulesCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var hotelRule = new HotelRules
				{
					Name = hotelRulesCreateDto.Name,
					Content = hotelRulesCreateDto.Content,
					HotelId = hotelRulesCreateDto.HotelId
				};

				await _appdbcontext.hotelRules.AddAsync(hotelRule, cancellationToken);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return hotelRule;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel rule");
				throw;
			}
		}

		public async Task<bool> DeleteHotelRulesAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var rule = await _appdbcontext.hotelRules
					.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

				if (rule == null)
					return false;

				_appdbcontext.hotelRules.Remove(rule);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel rule");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelRules>> GetAllHotelRulesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var rules = await _appdbcontext.hotelRules
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return rules;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotel rules");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<HotelRules>> GetHotelRulesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var rules = await _appdbcontext.hotelRules
					.Where(r => r.HotelId == hotelId)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return rules;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while retrieving hotel rules by hotel {hotelId}");
				throw;
			}
		}

		public async Task<HotelRules> UpdateHotelRulesAsync(HotelRulesUpdateDto hotelRulesUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var rule = await _appdbcontext.hotelRules
					.FirstOrDefaultAsync(r => r.Id == hotelRulesUpdateDto.Id, cancellationToken);

				if (rule == null)
					return null;

				rule.Name = hotelRulesUpdateDto.Name;
				rule.Content = hotelRulesUpdateDto.Content;

				_appdbcontext.hotelRules.Update(rule);
				await _appdbcontext.SaveChangesAsync(cancellationToken);

				return rule;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel rule");
				throw;
			}
		}
	}
}
