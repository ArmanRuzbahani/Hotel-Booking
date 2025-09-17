using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hotel_Booking_Application_Service.Services;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using Entitys_Hotel.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace XUNIT_TEST_BOOKING.Service.Customer
{
	public class CustomerServiceTests
	{
		private readonly Mock<ICustomerRepository> _repoMock;
		private readonly Mock<ILogger<CustomerService>> _loggerMock;
		private readonly CustomerService _service;

		public CustomerServiceTests()
		{
			_repoMock = new Mock<ICustomerRepository>();
			_loggerMock = new Mock<ILogger<CustomerService>>();
			_service = new CustomerService(_repoMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async Task GetCustomerForProfileById_ReturnsCustomer()
		{
			_repoMock.Setup(r => r.GetCustomerForProfileById(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(new List<CustomerReadDto> { new CustomerReadDto { Id = 1, Name = "Arman" } });

			var result = await _service.GetCustomerForProfileById(1, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
		}

		[Fact]
		public async Task GetAllCustomers_ReturnsList()
		{
			_repoMock.Setup(r => r.GetAllCustomers(It.IsAny<CancellationToken>()))
					 .ReturnsAsync(new List<CustomerReadDto> { new CustomerReadDto { Id = 1, Name = "Arman" } });

			var result = await _service.GetAllCustomers(CancellationToken.None);

			Assert.Single(result);
		}

		[Fact]
		public async Task CreateCustomer_CallsRepository()
		{
			var dto = new CustomerCreateDto { Name = "Arman", Email = "a@b.com", PhoneNumber = "09123456789", CardId = "123" };

			_repoMock.Setup(r => r.CreateCustomer(It.IsAny<CustomerCreateDto>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync(new Entitys_Hotel.Models.Customer { Id = 1, Name = "Arman" });

			await _service.CreateCustomer(dto, CancellationToken.None);

			_repoMock.Verify(r => r.CreateCustomer(dto, It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task UpdateCustomer_ReturnsTrue()
		{
			var dto = new CustomerUpdateDto { Id = 1, Name = "Arman", Email = "a@b.com", PhoneNumber = "09123456789", CardId = "123" };

			_repoMock.Setup(r => r.UpdateCustomer(It.IsAny<CustomerUpdateDto>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync(new Entitys_Hotel.Models.Customer { Id = 1, Name = "Arman" });

			var result = await _service.UpdateCustomer(dto, CancellationToken.None);

			Assert.True(result);
		}

		[Fact]
		public async Task DeleteCustomer_ReturnsTrue()
		{
			_repoMock.Setup(r => r.DeleteCustomer(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);

			var result = await _service.DeleteCustomer(1, CancellationToken.None);

			Assert.True(result);
		}
	}
}
