using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entitys_Hotel.Models;
using Hotel_Booking_Application_Service.Services;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace XUNIT_TEST_BOOKING.Service.HotelTests
{
	public class HotelServiceTests
	{
		private readonly Mock<IHotelRepository> _repoMock;
		private readonly Mock<ILogger<HotelService>> _loggerMock;
		private readonly HotelService _service;

		public HotelServiceTests()
		{
			_repoMock = new Mock<IHotelRepository>();
			_loggerMock = new Mock<ILogger<HotelService>>();
			_service = new HotelService(_repoMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async Task CreateHotelAsync_ValidHotel_ReturnsHotel()
		{
			var dto = new HotelCreateDto { Name = "Test Hotel", Stars = 4 };
			var hotel = new Hotel { Id = 1, Name = "Test Hotel" };

			_repoMock.Setup(r => r.CreateHotelAsync(dto, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(hotel);

			var result = await _service.CreateHotelAsync(dto, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal("Test Hotel", result.Name);
			Assert.Equal(1, result.Id);
		}

		[Fact]
		public async Task GetHotelByIdAsync_InvalidId_ReturnsNull()
		{
			var result = await _service.GetHotelByIdAsync(0, CancellationToken.None);
			Assert.Null(result);
		}

		[Fact]
		public async Task DeleteHotelAsync_ValidId_ReturnsTrue()
		{
			_repoMock.Setup(r => r.DeleteHotelAsync(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(true);

			var result = await _service.DeleteHotelAsync(1, CancellationToken.None);

			Assert.True(result);
		}

		[Fact]
		public async Task GetAllHotelsAsync_ReturnsHotels()
		{
			var hotels = new List<Hotel>
			{
				new Hotel { Id = 1, Name = "Hotel One" },
				new Hotel { Id = 2, Name = "Hotel Two" }
			};

			_repoMock.Setup(r => r.GetAllHotelsAsync(It.IsAny<CancellationToken>()))
					 .ReturnsAsync(hotels);

			var result = await _service.GetAllHotelsAsync(CancellationToken.None);

			Assert.Equal(2, result.Count);
			Assert.Contains(result, h => h.Name == "Hotel One");
		}

		[Fact]
		public async Task SearchHotelsAsync_ReturnsHotels()
		{
			var hotels = new List<Hotel> { new Hotel { Id = 1, Name = "SearchResult" } };

			_repoMock.Setup(r => r.SearchHotelsAsync("Search", It.IsAny<CancellationToken>()))
					 .ReturnsAsync(hotels);

			var result = await _service.SearchHotelsAsync("Search", CancellationToken.None);

			Assert.Single(result);
			Assert.Equal("SearchResult", result.First().Name); // ✅ use First() instead of [0]
		}

		[Fact]
		public async Task UpdateHotelAsync_ValidHotel_ReturnsHotel()
		{
			var dto = new HotelUpdateDto { Id = 1, Name = "Updated Hotel", Stars = 5 };
			var hotel = new Hotel { Id = 1, Name = "Updated Hotel" };

			_repoMock.Setup(r => r.UpdateHotelAsync(dto, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(hotel);

			var result = await _service.UpdateHotelAsync(dto, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal("Updated Hotel", result.Name);
		}
	}
}
