using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<CustomerRepository> _logger;

		public CustomerRepository(AppDbContext appDbContext,ILogger<CustomerRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}
		public Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
		{
			if (customerCreateDto == null)
			{
				throw new ArgumentNullException(nameof(customerCreateDto));
			}
			try
			{
				var Customer = new Customer 
				{
				 Name = customerCreateDto.Name,
				 LastName = customerCreateDto.LastName,
				 UserCreateAt= DateTime.UtcNow,
				 DateOfBirth = DateTime.UtcNow,
				 Education = customerCreateDto.Education,
				 Email = customerCreateDto.Email,
				 Job = customerCreateDto.Job,
				 MaritalStatus = customerCreateDto.MaritalStatus,
				 PhoneNumber = customerCreateDto.PhoneNumber,
				 Nationality = customerCreateDto.Nationality,
				 Gender = customerCreateDto.Gender,
				 IsActive = true,
				 
				
				}
			}
		}





















		public Task<bool> DeleteCustomer(int Id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<Customer>> GetAllCustomers(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Customer> GetCustomerForProfileById(int Id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Customer> UpdateCustomer(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
