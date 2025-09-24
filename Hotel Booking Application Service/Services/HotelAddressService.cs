using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.HotelAddress;
using Microsoft.Extensions.Logging;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelAddressService : IHotelAdressService
	{
		private readonly IHotelAddressRepository _hotelAddressRepository;
		private readonly ILogger<HotelAddressService> _logger;

		public HotelAddressService(IHotelAddressRepository hotelAddressRepository, ILogger<HotelAddressService> logger)
		{
			_hotelAddressRepository = hotelAddressRepository;
			_logger = logger;
		}

		public async Task<HotelAddress> CreateHotelAddressAsync(HotelAddressCreateDto hotelAddressCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelAddressCreateDto == null)
					throw new ArgumentNullException(nameof(hotelAddressCreateDto), "آدرس هتل نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(hotelAddressCreateDto.Address))
					throw new ArgumentException("آدرس نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(hotelAddressCreateDto.AddressName))
					throw new ArgumentException("نام آدرس نمی‌تواند خالی باشد.");

				return await _hotelAddressRepository.CreateHotelAddressAsync(hotelAddressCreateDto, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام ایجاد آدرس هتل");
				throw;
			}
		}

		public async Task<bool> DeleteHotelAddressAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
					throw new ArgumentException("شناسه آدرس هتل معتبر نیست.");

				var result = await _hotelAddressRepository.DeleteHotelAddressAsync(id, cancellationToken);

				if (!result)
					throw new KeyNotFoundException("آدرس هتل مورد نظر یافت نشد.");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در هنگام حذف آدرس هتل با شناسه {id}");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelAddress>> GetAllHotelAddressesAsync(CancellationToken cancellationToken)
		{
			try
			{
				var addresses = await _hotelAddressRepository.GetAllHotelAddressesAsync(cancellationToken);

				if (addresses == null || addresses.Count == 0)
					throw new KeyNotFoundException("هیچ آدرس هتلی در سیستم یافت نشد.");

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام دریافت همه آدرس‌های هتل");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelAddress?>> GetHotelAddressesByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
					throw new ArgumentException("شناسه هتل معتبر نیست.");

				var addresses = await _hotelAddressRepository.GetHotelAddressesByHotelIdAsync(hotelId, cancellationToken);

				if (addresses == null || addresses.Count == 0)
					throw new KeyNotFoundException("هیچ آدرسی برای این هتل یافت نشد.");

				return addresses;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"خطا در هنگام دریافت آدرس‌های هتل با شناسه {hotelId}");
				throw;
			}
		}

		public async Task<HotelAddress> UpdateHotelAddressAsync(HotelAddressUpdateDto hotelAddressUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelAddressUpdateDto == null)
					throw new ArgumentNullException(nameof(hotelAddressUpdateDto), "اطلاعات آدرس هتل نمی‌تواند خالی باشد.");

				if (hotelAddressUpdateDto.Id <= 0)
					throw new ArgumentException("شناسه آدرس هتل معتبر نیست.");

				if (string.IsNullOrWhiteSpace(hotelAddressUpdateDto.Address))
					throw new ArgumentException("آدرس نمی‌تواند خالی باشد.");

				if (string.IsNullOrWhiteSpace(hotelAddressUpdateDto.AddressName))
					throw new ArgumentException("نام آدرس نمی‌تواند خالی باشد.");

				var updatedAddress = await _hotelAddressRepository.UpdateHotelAddressAsync(hotelAddressUpdateDto, cancellationToken);

				if (updatedAddress == null)
					throw new KeyNotFoundException("آدرس هتل مورد نظر برای بروزرسانی یافت نشد.");

				return updatedAddress;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در هنگام بروزرسانی آدرس هتل");
				throw;
			}
		}
	}
}
