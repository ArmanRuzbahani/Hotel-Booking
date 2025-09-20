using Microsoft.Extensions.Logging;
using Hotel_Booking_Infrastruters.Common;
using Hotel_Booking_Infrastruters.Repository;
using Hotel_Booking_Application_Service.Services;

namespace Hotel_Booking_Console_Test
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// ساخت DbContext مستقیم
			var dbContext = new AppDbContext();

			// ساخت Logger ساده
			using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			var repoLogger = loggerFactory.CreateLogger<HotelRepository>();
			var serviceLogger = loggerFactory.CreateLogger<HotelService>();

			// ساخت Repository و Service
			var hotelRepository = new HotelRepository(dbContext, repoLogger);
			var hotelService = new HotelService(hotelRepository, serviceLogger);

			// تست
			Console.WriteLine("=== Hotel Search Test ===");

			try
			{
				// همه هتل‌ها
				Console.WriteLine("\n1. All Hotels:");
				var allHotels = await hotelService.GetAllHotelsAsync(CancellationToken.None);
				foreach (var hotel in allHotels)
				{
					Console.WriteLine($"- {hotel.Name} ({hotel.Stars} stars)");
				}

				// جستجو
				Console.WriteLine("\n2. Search Test:");
				Console.Write("Enter search term: ");
				var searchTerm = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(searchTerm))
				{
					var searchResults = await hotelService.SearchHotelsAsync(searchTerm, CancellationToken.None);
					Console.WriteLine($"\nFound {searchResults.Count} hotels:");
					foreach (var hotel in searchResults)
					{
						Console.WriteLine($"- {hotel.Name} in {hotel.iranCityForHotel} ({hotel.Stars} stars)");
					}

					// جستجوی پیشرفته
					Console.WriteLine("\n3. Advanced Search Test (3+ stars):");
					var advancedResults = await hotelService.SearchHotelsAdvancedAsync(
						searchTerm, minStars: 3, pageSize: 10);

					foreach (var hotel in advancedResults)
					{
						Console.WriteLine($"- {hotel.Name} ({hotel.Stars} stars)");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}

			Console.WriteLine("\nTest completed! Press any key to exit...");
			Console.ReadKey();
		}
	}
}