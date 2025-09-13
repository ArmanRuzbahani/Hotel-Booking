using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Food;
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
	public class FoodRepository : IFoodRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<FoodRepository> _logger;

		public FoodRepository(AppDbContext appDbContext, ILogger<FoodRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}
		public async Task<Food> CreateFoodAsync(FoodCreateDto foodCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var food = new Food
				{
					Name = foodCreateDto.Name,
					Description = foodCreateDto.Description,
					Picture = foodCreateDto.Picture
				};

				await _appDbContext.food.AddAsync(food, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return food;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating the food item");
				throw;
			}
		}

		public async Task<bool> DeleteFoodAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var food = await _appDbContext.food
					.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

				if (food == null)
				{
					return false;
				}

				_appDbContext.food.Remove(food);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting food item");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<Food>> GetAllFoodsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var foods = await _appDbContext.food
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return foods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all foods");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Food?>> GetFoodsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var foods = await _appDbContext.food
					.Where(h => h.Id == hotelId)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return foods;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving foods by hotel ID");
				throw;
			}
		}

		public async Task<Food> UpdateFoodAsync(FoodUpdateDto foodUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var food = await _appDbContext.food
					.FirstOrDefaultAsync(f => f.Id == foodUpdateDto.Id, cancellationToken);

				if (food == null)
				{
					return null;
				}

				food.Name = foodUpdateDto.Name;
				food.Description = foodUpdateDto.Description;
				food.Picture = foodUpdateDto.Picture;

				_appDbContext.food.Update(food);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return food;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating food item");
				throw;
			}
		}
	}
}
