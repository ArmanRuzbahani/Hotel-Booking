using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class AddressService : IAddressService
	{
		private readonly IAddressService _addressService;
		private readonly ILogger<AddressService> _logger;

		public AddressService(IAddressService addressService, ILogger<AddressService> logger)
		{
			_addressService = addressService;
			_logger = logger;
		}

		public Task<Address> CreateAddressAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<AddressReadDto?>> GetAddressesByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<AddressReadDto?>> GetAllAddressesAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Address> UpdateAddressAsync(AddressUpdateDto addressUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
