using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Address;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class AddressService : IAddressService
	{
		private readonly IAddressRepository _addressRepository;
		private readonly ILogger<AddressService> _logger;

		public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
		{
			_addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<Address> CreateAddressAsync(AddressCreateDto addressCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (addressCreateDto == null)
					throw new ArgumentNullException(nameof(addressCreateDto), "اطلاعات آدرس نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(addressCreateDto.AddressName))
					throw new ArgumentException("نام آدرس نمی‌تواند خالی باشد.");

				if (addressCreateDto.CustomerId <= 0)
					throw new ArgumentException("شناسه مشتری معتبر نیست.");

				return await _addressRepository.CreateAddressAsync(addressCreateDto, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد آدرس");
				throw;
			}
		}

		public async Task<Address> UpdateAddressAsync(AddressUpdateDto addressUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (addressUpdateDto == null)
					throw new ArgumentNullException(nameof(addressUpdateDto), "اطلاعات آدرس برای بروزرسانی نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(addressUpdateDto.AddressName))
					throw new ArgumentException("نام آدرس نمی‌تواند خالی باشد.");

				if (addressUpdateDto.CustomerId <= 0)
					throw new ArgumentException("شناسه مشتری معتبر نیست.");

				var updated = await _addressRepository.UpdateAddressAsync(addressUpdateDto, cancellationToken);

				if (updated == null)
					throw new KeyNotFoundException("آدرس مورد نظر برای بروزرسانی یافت نشد.");

				return updated;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در بروزرسانی آدرس با شناسه {addressUpdateDto.Id}");
				throw;
			}
		}

		public async Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه آدرس معتبر نیست.");

				var deleted = await _addressRepository.DeleteAddressAsync(id, cancellationToken);

				if (!deleted)
					throw new KeyNotFoundException("آدرس مورد نظر برای حذف یافت نشد.");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در حذف آدرس با شناسه {id}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<AddressReadDto?>> GetAddressesByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				if (customerId <= 0)
					throw new ArgumentException("شناسه مشتری معتبر نیست.");

				var addresses = await _addressRepository.GetAddressesByCustomerIdAsync(customerId, cancellationToken);

				if (addresses == null || !addresses.Any())
					throw new KeyNotFoundException("هیچ آدرسی برای این مشتری یافت نشد.");

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در دریافت آدرس‌ها برای مشتری با شناسه {customerId}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<AddressReadDto?>> GetAllAddressesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _addressRepository.GetAllAddressesAsync(cancellationToken);

				if (addresses == null || !addresses.Any())
					throw new KeyNotFoundException("هیچ آدرسی در سیستم یافت نشد.");

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت همه آدرس‌ها");
				throw;
			}
		}
	}
}
