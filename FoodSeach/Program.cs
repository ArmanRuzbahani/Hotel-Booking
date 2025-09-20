using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.Entitys;
using Hotel_Booking_Application_Service.Services;
using Hotel_Booking_Infrastruters.Common;
using Hotel_Booking_Infrastruters.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Console_Test
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Create a simple Logger
			using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			var repoLogger = loggerFactory.CreateLogger<FoodRepository>();
			var serviceLogger = loggerFactory.CreateLogger<FoodService>();

			// Create DbContext (replace with your actual DbContext configuration)
			var dbContext = new AppDbContext(); // Ensure AppDbContext is properly configured

			// Create Repository and Service
			IFoodRepository foodRepository = new FoodRepository(dbContext, repoLogger); // Use actual FoodRepository
			var foodService = new FoodService(foodRepository, serviceLogger);

			// Test
			Console.WriteLine("=== Food Search Test ===");

			try
			{
				// Test 1: Basic search
				Console.WriteLine("\n1. Search Test:");
				Console.Write("Enter search term: ");
				var searchTerm = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(searchTerm))
				{
					var searchResults = await foodService.SearchFoodsAsync(searchTerm, CancellationToken.None);
					Console.WriteLine($"\nFound {searchResults.Count} foods:");
					if (searchResults.Count == 0)
					{
						Console.WriteLine("No foods found for the search term.");
					}
					else
					{
						foreach (var food in searchResults)
						{
							Console.WriteLine($"- {food.Name}");
						}
					}

					// Test 2: Advanced search
					Console.WriteLine("\n2. Advanced Search Test (by Hotel ID):");
					Console.Write("Enter Hotel ID (or leave blank for all hotels): ");
					var hotelIdInput = Console.ReadLine();
					int? hotelId = null;
					if (!string.IsNullOrWhiteSpace(hotelIdInput) && int.TryParse(hotelIdInput, out var parsedHotelId))
					{
						hotelId = parsedHotelId;
					}

					var advancedResults = await foodService.SearchFoodsAdvancedAsync(
						searchTerm, hotelId, pageSize: 10, CancellationToken.None);

					Console.WriteLine($"\nFound {advancedResults.Count} foods:");
					if (advancedResults.Count == 0)
					{
						Console.WriteLine("No foods found for the advanced search criteria.");
					}
					else
					{
						foreach (var food in advancedResults)
						{
							Console.WriteLine($"- {food.Name}");
						}
					}
				}
				else
				{
					Console.WriteLine("Search term cannot be empty.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
				}
			}

			Console.WriteLine("\nTest completed! Press any key to exit...");
			Console.ReadKey();
		}
	}
}