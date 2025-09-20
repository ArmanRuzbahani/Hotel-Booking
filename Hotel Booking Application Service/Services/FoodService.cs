using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Food;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class FoodService : IFoodService
	{
		private readonly IFoodRepository _foodRepository;
		private readonly ILogger<FoodService> _logger;

		public FoodService(IFoodRepository foodRepository, ILogger<FoodService> logger)
		{
			_foodRepository = foodRepository;
			_logger = logger;
		}

		public Task<Food> CreateFoodAsync(FoodCreateDto foodCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteFoodAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Food>> GetAllFoodsAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Food?>> GetFoodsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Food> UpdateFoodAsync(FoodUpdateDto foodUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<IReadOnlyCollection<Food>> SearchFoodsAsync(string searchTerm, CancellationToken cancellationToken)
		{
			var foods = await _foodRepository.SearchFoodsAsync(searchTerm, cancellationToken);
			return foods.ToList().AsReadOnly();
		}

		public async Task<IReadOnlyCollection<Food>> SearchFoodsAdvancedAsync(string searchTerm, int? hotelId = null, int pageSize = 50, CancellationToken cancellationToken = default)
		{
			var foods = await _foodRepository.SearchFoodsAdvancedAsync(searchTerm, hotelId, pageSize, cancellationToken);
			return foods.ToList().AsReadOnly();
		}
	}
}
