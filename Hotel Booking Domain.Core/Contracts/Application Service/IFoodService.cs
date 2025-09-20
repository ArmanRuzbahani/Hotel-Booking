using Hotel_Booking_Domain.Core.DTO.Repository.Food;
using Hotel_Booking_Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IFoodService
	{
		Task<IReadOnlyCollection<Food>> GetAllFoodsAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Food?>> GetFoodsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<Food> CreateFoodAsync(FoodCreateDto foodCreateDto, CancellationToken cancellationToken);

		Task<Food> UpdateFoodAsync(FoodUpdateDto foodUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteFoodAsync(int id, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Food>> SearchFoodsAsync(string searchTerm, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<Food>> SearchFoodsAdvancedAsync(string searchTerm, int? hotelId = null, int pageSize = 50, CancellationToken cancellationToken = default);
	}
}
