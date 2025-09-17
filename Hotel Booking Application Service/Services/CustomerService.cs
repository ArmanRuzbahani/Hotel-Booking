using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly ILogger<CustomerService> _logger;

		public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
		{
			_customerRepository = customerRepository;
			_logger = logger;
		}
		public async Task CreateCustomer(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (customerCreateDto == null)
					throw new ArgumentNullException(nameof(customerCreateDto), "اطلاعات مشتری نمی‌تواند خالی باشد.");

				
				if (string.IsNullOrWhiteSpace(customerCreateDto.Email) ||
					!Regex.IsMatch(customerCreateDto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
				{
					throw new ArgumentException("ایمیل وارد شده معتبر نیست.");
				}

				
				if (string.IsNullOrWhiteSpace(customerCreateDto.PhoneNumber) ||
					!Regex.IsMatch(customerCreateDto.PhoneNumber, @"^09\d{9}$"))
				{
					throw new ArgumentException("شماره موبایل باید یک شماره معتبر ایرانی باشد (مثال: 09123456789).");
				}

				
				if (string.IsNullOrWhiteSpace(customerCreateDto.CardId))
				{
					throw new ArgumentException("کد ملی نمی‌تواند خالی باشد.");
				}

				await _customerRepository.CreateCustomer(customerCreateDto, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "eror in Service layer when creating Customer");
				throw;
			}
		}
		public async Task<bool> DeleteCustomer(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه کاربر معتبر نیست.");

				var deleted = await _customerRepository.DeleteCustomer(id, cancellationToken);

				if (!deleted)
					throw new KeyNotFoundException("کاربر مورد نظر برای حذف پیدا نشد.");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Eror when Deleting Customer with id {id}");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<CustomerReadDto>> GetAllCustomers(CancellationToken cancellationToken)
		{
			try
			{
				var customers = await _customerRepository.GetAllCustomers(cancellationToken);

				if (customers == null || customers.Count == 0)
					throw new KeyNotFoundException("هیچ کاربری‌ در سیستم یافت نشد.");

				return customers;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Eror in Service Layer when to Get All the Cusomers");
				throw;
			}
		}


		public async Task<CustomerReadDto> GetCustomerForProfileById(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه کاربر معتبر نیست.");

				var customers = await _customerRepository.GetCustomerForProfileById(id, cancellationToken);

				var customer = customers.FirstOrDefault();
				if (customer == null)
					throw new KeyNotFoundException("کاربر با شناسه وارد شده یافت نشد.");

				return customer;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Eror in Servise layer when To Get Customer with Id {id}");
				throw;
			}
		}


		public async Task<bool> UpdateCustomer(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (customerUpdateDto == null)
					throw new ArgumentNullException(nameof(customerUpdateDto), "اطلاعات کاربر برای بروزرسانی نمی‌تواند خالی باشد.");

				
				if (string.IsNullOrWhiteSpace(customerUpdateDto.Email) ||
					!Regex.IsMatch(customerUpdateDto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
				{
					throw new ArgumentException("ایمیل وارد شده معتبر نیست.");
				}

				
				if (string.IsNullOrWhiteSpace(customerUpdateDto.PhoneNumber) ||
					!Regex.IsMatch(customerUpdateDto.PhoneNumber, @"^09\d{9}$"))
				{
					throw new ArgumentException("شماره موبایل باید یک شماره معتبر ایرانی باشد (مثال: 09123456789).");
				}

				
				if (string.IsNullOrWhiteSpace(customerUpdateDto.CardId))
				{
					throw new ArgumentException("کد ملی نمی‌تواند خالی باشد.");
				}

				
				var updated = await _customerRepository.UpdateCustomer(customerUpdateDto, cancellationToken);

				if (updated == null)
					throw new KeyNotFoundException("کاربر مورد نظر برای بروزرسانی پیدا نشد.");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"eror in service layer when updating Customer with Id {customerUpdateDto.Id}");
				throw;
			}
	    }
	}
}
