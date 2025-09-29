using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
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
		public async Task<Customer> CreateCustomer(CustomerCreateDto customerCreateDto, CancellationToken cancellationToken)
		{
			if (customerCreateDto == null)
				throw new ArgumentNullException(nameof(customerCreateDto));

			try
			{
				var customer = new Customer
				{
					Name = customerCreateDto.Name,
					LastName = customerCreateDto.LastName,
					Email = customerCreateDto.Email,
					PhoneNumber = customerCreateDto.PhoneNumber,
					CardId = customerCreateDto.CardId,
					Job = customerCreateDto.Job,
					MaritalStatus = customerCreateDto.MaritalStatus,
					Gender = customerCreateDto.Gender,
					Education = customerCreateDto.Education,
					Nationality = customerCreateDto.Nationality,
					UserCreateAt = DateTime.UtcNow,
					DateOfBirth = customerCreateDto.DateOfBirth,
					City = customerCreateDto.city,
					Role = customerCreateDto.Role,
					IsActive = true,
					IsPhoneNumberVerfied = false
				};

				
				await _appDbContext.customers.AddAsync(customer, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return customer;
			}
			catch (DbUpdateException dbEx)
			{
				
				throw new InvalidOperationException("An error occurred while saving the customer to the database.", dbEx);
			}
			catch (OperationCanceledException)
			{
				
				throw new TaskCanceledException("Customer creation was canceled.");
			}
			catch (Exception ex)
			{
				
				throw new Exception("An unexpected error occurred while creating the customer.", ex);
			}
		}

		public async Task<bool> DeleteCustomer(int id, CancellationToken cancellationToken)
		{
			try
			{
				var customer = await _appDbContext.customers
					.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

				if (customer == null)
					return false;

				_appDbContext.customers.Remove(customer);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (DbUpdateException dbEx)
			{
				throw new InvalidOperationException("An error occurred while deleting the customer from the database.", dbEx);
			}
			catch (OperationCanceledException)
			{
				throw new TaskCanceledException("The delete operation was canceled.");
			}
			catch (Exception ex)
			{
				throw new Exception("An unexpected error occurred while deleting the customer.", ex);
			}
		}

		public async Task<IReadOnlyCollection<CustomerReadDto>> GetAllCustomers(CancellationToken cancellationToken)
		{
			try
			{
				var customers = await _appDbContext.customers
					.AsNoTracking()
					.Select(c => new CustomerReadDto
					{
						Id = c.Id,
						Name = c.Name,
						LastName = c.LastName,
						Email = c.Email,
						PhoneNumber = c.PhoneNumber,
						DateOfBirth = c.DateOfBirth,
						CardId = c.CardId,
						Job = c.Job,
						MaritalStatus = c.MaritalStatus,
						Gender = c.Gender,
						Education = c.Education,
						Nationality = c.Nationality,
						CustomerCity = c.City,
						Role = c.Role,
						IsActive = c.IsActive,
						IsPhoneNumberVerfied = c.IsPhoneNumberVerfied,
						UserCreateAt = c.UserCreateAt
					})
					.ToListAsync(cancellationToken);

				if (customers == null || customers.Count == 0)
				{
					return Array.Empty<CustomerReadDto>();
				}
				else
				{
					return customers;
				}
			}
			catch (DbUpdateException dbEx)
			{
				throw new InvalidOperationException("An error occurred while fetching customers from the database.", dbEx);
			}
			catch (OperationCanceledException)
			{
				throw new TaskCanceledException("The operation was canceled.");
			}
			catch (Exception ex)
			{
				throw new Exception("An unexpected error occurred while fetching customers.", ex);
			}
		}



		public async Task<IReadOnlyCollection<CustomerReadDto>> GetCustomerForProfileById(int id, CancellationToken cancellationToken)
		{
			try
			{
				var customer = await _appDbContext.customers
					.AsNoTracking()
					.Where(c => c.Id == id)
					.Select(c => new CustomerReadDto
					{
						Id = c.Id,
						Name = c.Name,
						LastName = c.LastName,
						Email = c.Email,
						PhoneNumber = c.PhoneNumber,
						DateOfBirth = c.DateOfBirth,
						CardId = c.CardId,
						Job = c.Job,
						MaritalStatus = c.MaritalStatus,
						Gender = c.Gender,
						Education = c.Education,
						Nationality = c.Nationality,
						CustomerCity = c.City,
						Role = c.Role,
						IsActive = c.IsActive,
						IsPhoneNumberVerfied = c.IsPhoneNumberVerfied,
						UserCreateAt = c.UserCreateAt
					})
					.ToListAsync(cancellationToken);

				if (customer == null || customer.Count == 0)
				{
					return Array.Empty<CustomerReadDto>();
				}
				else
				{
					return customer;
				}
			}
			catch (DbUpdateException dbEx)
			{
				throw new InvalidOperationException("An error occurred while fetching the customer profile.", dbEx);
			}
			catch (OperationCanceledException)
			{
				throw new TaskCanceledException("The operation was canceled.");
			}
			catch (Exception ex)
			{
				throw new Exception("An unexpected error occurred while fetching the customer profile.", ex);
			}
		}

		public async Task<bool> PhonNumberVerfiedManagement(int UserId, CancellationToken cancellationToken)
		{
			try
			{
				if (UserId <= 0)
				{
					_logger.LogWarning("Invalid userId: {UserId}", UserId);
					throw new ArgumentException("User does not exist", nameof(UserId));
				}

				var result = await _appDbContext.customers
					.FirstOrDefaultAsync(c => c.Id == UserId, cancellationToken);

				if (result == null)
				{
					_logger.LogWarning("Customer not found with userId: {UserId}", UserId);
					throw new KeyNotFoundException($"Customer with userId {UserId} was not found.");
				}

				result.IsPhoneNumberVerfied = true;

				await _appDbContext.SaveChangesAsync(cancellationToken);

				_logger.LogInformation("Phone number verified successfully for userId: {UserId}", UserId);

				return true;
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Invalid argument in PhoneNumberVerifiedManagement");
				throw; 
			}
			catch (KeyNotFoundException ex)
			{
				_logger.LogError(ex, "User not found in PhoneNumberVerifiedManagement");
				throw; 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while verifying phone number for userId: {UserId}", UserId);
				return false;
			}
		}






		public async Task<Customer> UpdateCustomer(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var customer = await _appDbContext.customers
					.FirstOrDefaultAsync(c => c.Id == customerUpdateDto.Id, cancellationToken);

				if (customer == null)
				{
					return null;
				}
				else
				{
					customer.Name = customerUpdateDto.Name;
					customer.LastName = customerUpdateDto.LastName;
					customer.Email = customerUpdateDto.Email;
					customer.PhoneNumber = customerUpdateDto.PhoneNumber;
					customer.CardId = customerUpdateDto.CardId;
					customer.Job = customerUpdateDto.Job;
					customer.MaritalStatus = customerUpdateDto.MaritalStatus;
					customer.Gender = customerUpdateDto.Gender;
					customer.Education = customerUpdateDto.Education;
					customer.Nationality = customerUpdateDto.Nationality;
					customer.City = customerUpdateDto.city;
					customer.Role = customerUpdateDto.Role;
					_appDbContext.customers.Update(customer);
					await _appDbContext.SaveChangesAsync(cancellationToken);

					return customer;
				}
			}
			catch (DbUpdateException dbEx)
			{
				throw new InvalidOperationException("An error occurred while updating the customer.", dbEx);
			}
			catch (OperationCanceledException)
			{
				throw new TaskCanceledException("The update operation was canceled.");
			}
			catch (Exception ex)
			{
				throw new Exception("An unexpected error occurred while updating the customer.", ex);
			}
		}

	}
}
