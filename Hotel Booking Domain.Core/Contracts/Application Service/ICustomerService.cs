using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface ICustomerService
	{
		Task CreateCustomer(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken);

		Task<bool> UpdateCustomer(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<CustomerReadDto>> GetAllCustomers(CancellationToken cancellationToken);

		Task<CustomerReadDto> GetCustomerForProfileById(int id, CancellationToken cancellationToken);

		Task<bool> DeleteCustomer(int id, CancellationToken cancellationToken);
	}
}
