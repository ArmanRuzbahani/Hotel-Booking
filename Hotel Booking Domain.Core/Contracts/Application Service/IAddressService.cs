using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IAddressService
	{
		Task<IReadOnlyCollection<AddressReadDto?>> GetAllAddressesAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<AddressReadDto?>> GetAddressesByCustomerIdAsync(int customerId, CancellationToken cancellationToken);

		Task<Address> CreateAddressAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken);

		Task<Address> UpdateAddressAsync(AddressUpdateDto addressUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken);
	}
}
