using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface ICustomerRepository
	{
		Task<IReadOnlyCollection<CustomerReadDto>> GetAllCustomers(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<CustomerReadDto>> GetCustomerForProfileById(int Id,CancellationToken cancellationToken);

		Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken);

		Task<Customer> UpdateCustomer(CustomerUpdateDto customerUpdateDto ,CancellationToken cancellationToken);

		Task<bool> DeleteCustomer(int Id,CancellationToken cancellationToken);

		Task<bool> PhonNumberVerfiedManagement(int UserId, CancellationToken cancellationToken);
	}
}
