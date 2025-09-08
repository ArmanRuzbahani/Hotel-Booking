using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using Hotel_Booking_Domain.Core.DTO.Repository.Customer;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class AddressRepository : IAddressRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<AddressRepository> _logger;

		public AddressRepository(AppDbContext appDbContext, ILogger<AddressRepository> logger)
		{
			_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//Create
		public async Task<Address> CreateAddressAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken)
		{
			if (addressCreateDto == null)
				throw new ArgumentNullException(nameof(addressCreateDto));

			if (string.IsNullOrWhiteSpace(addressCreateDto.AddressName))
				throw new ArgumentException("Address name cannot be null or empty");

			try
			{
				var addressExists = await _appDbContext.addresses
					.AnyAsync(a => a.CustomerId == addressCreateDto.CustomerId
								&& a.AddressName == addressCreateDto.AddressName, cancellationToken);

				if (addressExists)
					throw new ArgumentException($"Address '{addressCreateDto.AddressName}' already exists for customer {addressCreateDto.CustomerId}");

				var newAddress = new Address
				{
					AddressName = addressCreateDto.AddressName,
					CustomerId = addressCreateDto.CustomerId,
				};

				await _appDbContext.addresses.AddAsync(newAddress, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return newAddress;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while creating address for customer {CustomerId}",
					addressCreateDto.CustomerId);

				throw new InvalidOperationException("An error occurred while saving the address to the database", ex);
			}
			catch (ArgumentException)
			{
				throw; 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while creating address");
				throw new InvalidOperationException("An unexpected error occurred while creating the address", ex);
			}
		}

		//DElETE
		public async Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var address = await _appDbContext.addresses
					.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

				if (address == null)
					throw new ArgumentException($"Address with ID {id} not found.");

				_appDbContext.addresses.Remove(address);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while deleting address with ID {AddressId}", id);
				throw new InvalidOperationException("An error occurred while deleting the address from the database", ex);
			}
			catch (ArgumentException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while deleting address with ID {AddressId}", id);
				throw new InvalidOperationException("An unexpected error occurred while deleting the address", ex);
			}
		}



		public async Task<IReadOnlyCollection<AddressReadDto?>> GetAddressesByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _appDbContext.addresses
					.AsNoTracking()
					.Where(a => a.CustomerId == customerId)
					.Select(a => a.Customer != null ? new AddressReadDto
					{
						Id = a.Id,
						AddressName = a.AddressName,
						CustomerId = a.CustomerId,
						customerReadDto = new CustomerReadDto
						{
							Id = a.Customer.Id,
							Name = a.Customer.Name,
							LastName = a.Customer.LastName,
							Email = a.Customer.Email,
							DateOfBirth = a.Customer.DateOfBirth,
							Age = a.Customer.Age,
							PhoneNumber = a.Customer.PhoneNumber,
							CardId = a.Customer.CardId,
							UserCreateAt = a.Customer.UserCreateAt,
							IsActive = a.Customer.IsActive,
							Job = a.Customer.Job,
							MaritalStatus = a.Customer.MaritalStatus,
							Gender = a.Customer.Gender,
							Education = a.Customer.Education,
							Nationality = a.Customer.Nationality,
							CustomerCity = a.Customer.City
						}
					} : null)
					.ToListAsync(cancellationToken);
				_logger.LogInformation($"Retrieved {addresses} addresses for customer {customerId}", addresses, customerId);
				return addresses;
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Address retrieval for customer {CustomerId} was cancelled", customerId);
				throw;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching addresses for customer {CustomerId}", customerId);
				throw new InvalidOperationException("Failed to retrieve addresses due to a database error.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching addresses for customer {CustomerId}", customerId);
				throw new InvalidOperationException("An unexpected error occurred while retrieving addresses.", ex);
			}
		}

		public async Task<IReadOnlyCollection<AddressReadDto?>> GetAllAddressesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _appDbContext.addresses
					.AsNoTracking()
					.Select(x => new AddressReadDto
					{
						CustomerId = x.CustomerId,
						AddressName = x.AddressName,
						Id = x.Id,
						
						customerReadDto = x.Customer != null ? new CustomerReadDto
						{
							Id = x.Customer.Id,
							Name = x.Customer.Name,
							LastName = x.Customer.LastName,
							Email = x.Customer.Email,
							DateOfBirth = x.Customer.DateOfBirth,
							Age = x.Customer.Age,
							PhoneNumber = x.Customer.PhoneNumber,
							CardId = x.Customer.CardId,
							UserCreateAt = x.Customer.UserCreateAt,
							IsActive = x.Customer.IsActive,
							Job = x.Customer.Job,
							MaritalStatus = x.Customer.MaritalStatus,
							Gender = x.Customer.Gender,
							Education = x.Customer.Education,
							Nationality = x.Customer.Nationality,
							CustomerCity = x.Customer.City
						} : null
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {AddressCount} addresses", addresses.Count);
				return addresses;
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Address retrieval was cancelled");
				throw;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching addresses");
				throw new InvalidOperationException("Failed to retrieve addresses due to a database error.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching addresses");
				throw new InvalidOperationException("An unexpected error occurred while retrieving addresses.", ex);
			}
		}

		public async Task<Address> UpdateAddressAsync(AddressUpdateDto addressUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var adresforupdate = await _appDbContext.addresses.FirstOrDefaultAsync(x => x.Id == addressUpdateDto.Id, cancellationToken);
				if (adresforupdate != null)
				{
					adresforupdate.Id = addressUpdateDto.Id;
					adresforupdate.AddressName = addressUpdateDto.AddressName;
					adresforupdate.CustomerId = addressUpdateDto.CustomerId;

					await _appDbContext.SaveChangesAsync(cancellationToken);
					_logger.LogInformation("Address with ID {AddressId} updated successfully", adresforupdate.Id);
					return adresforupdate;
				}
				else
				{
					_logger.LogWarning("Address with ID {AddressId} not found for update", addressUpdateDto.Id);
					throw new InvalidOperationException($"Address with ID {addressUpdateDto.Id} not found.");
				}
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Address update for ID {AddressId} was cancelled", addressUpdateDto.Id);
				throw;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while updating address with ID {AddressId}", addressUpdateDto.Id);
				throw new InvalidOperationException("Failed to update address due to a database error.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while updating address with ID {AddressId}", addressUpdateDto.Id);
				throw new InvalidOperationException("An unexpected error occurred while updating address.", ex);
			}
		}
	}
}
