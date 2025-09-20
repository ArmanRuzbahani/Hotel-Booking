using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Hotel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class HotelService : IHotelService
	{
		private readonly IHotelRepository _hotelRepository;
		private readonly ILogger<HotelService> _logger;

		public HotelService(IHotelRepository hotelRepository, ILogger<HotelService> logger)
		{
			_hotelRepository = hotelRepository;
			_logger = logger;
		}

		public async Task<Hotel> CreateHotelAsync(HotelCreateDto hotelCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Creating hotel with name: {HotelName}", hotelCreateDto.Name);

				// اعتبارسنجی
				ValidateHotelCreateDto(hotelCreateDto);

				var result = await _hotelRepository.CreateHotelAsync(hotelCreateDto, cancellationToken);

				_logger.LogInformation("Hotel created successfully with ID: {HotelId}", result.Id);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel in service layer");
				throw;
			}
		}

		public async Task<bool> DeleteHotelAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Deleting hotel with ID: {HotelId}", id);

				if (id <= 0)
				{
					_logger.LogWarning("Invalid hotel ID provided: {HotelId}", id);
					return false;
				}

				var result = await _hotelRepository.DeleteHotelAsync(id, cancellationToken);

				if (result)
				{
					_logger.LogInformation("Hotel deleted successfully with ID: {HotelId}", id);
				}
				else
				{
					_logger.LogWarning("Hotel not found for deletion with ID: {HotelId}", id);
				}

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel with ID: {HotelId}", id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Retrieving all hotels");

				var result = await _hotelRepository.GetAllHotelsAsync(cancellationToken);

				_logger.LogInformation("Retrieved {HotelCount} hotels", result.Count);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotels in service layer");
				throw;
			}
		}

		public async Task<Hotel?> GetHotelByIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Retrieving hotel with ID: {HotelId}", hotelId);

				if (hotelId <= 0)
				{
					_logger.LogWarning("Invalid hotel ID provided: {HotelId}", hotelId);
					return null;
				}

				var result = await _hotelRepository.GetHotelByIdAsync(hotelId, cancellationToken);

				if (result == null)
				{
					_logger.LogWarning("Hotel not found with ID: {HotelId}", hotelId);
				}
				else
				{
					_logger.LogInformation("Hotel retrieved successfully with ID: {HotelId}", hotelId);
				}

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving hotel with ID: {HotelId}", hotelId);
				throw;
			}
		}

		public async Task<Hotel> UpdateHotelAsync(HotelUpdateDto hotelUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Updating hotel with ID: {HotelId}", hotelUpdateDto.Id);

				// اعتبارسنجی
				ValidateHotelUpdateDto(hotelUpdateDto);

				var result = await _hotelRepository.UpdateHotelAsync(hotelUpdateDto, cancellationToken);

				if (result == null)
				{
					_logger.LogWarning("Hotel not found for update with ID: {HotelId}", hotelUpdateDto.Id);
					return null;
				}

				_logger.LogInformation("Hotel updated successfully with ID: {HotelId}", result.Id);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel with ID: {HotelId}", hotelUpdateDto.Id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAsync(string searchTerm, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Searching hotels with term: {SearchTerm}", searchTerm);

				var result = await _hotelRepository.SearchHotelsAsync(searchTerm, cancellationToken);

				_logger.LogInformation("Found {HotelCount} hotels for search term: {SearchTerm}", result.Count, searchTerm);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while searching hotels with term: {SearchTerm}", searchTerm);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<Hotel>> SearchHotelsAdvancedAsync(
			string searchTerm,
			int? minStars = null,
			int? maxStars = null,
			int pageSize = 50,
			CancellationToken cancellationToken = default)
		{
			try
			{
				_logger.LogInformation("Advanced search - Term: {SearchTerm}, MinStars: {MinStars}, MaxStars: {MaxStars}, PageSize: {PageSize}",
					searchTerm, minStars, maxStars, pageSize);

				// اعتبارسنجی پارامترها
				if (pageSize <= 0 || pageSize > 100)
				{
					pageSize = 50;
					_logger.LogWarning("Invalid page size adjusted to default: 50");
				}

				if (minStars.HasValue && minStars.Value < 0)
				{
					minStars = 0;
					_logger.LogWarning("Invalid minStars adjusted to 0");
				}

				if (maxStars.HasValue && maxStars.Value < 0)
				{
					maxStars = null;
					_logger.LogWarning("Invalid maxStars set to null");
				}

				if (minStars.HasValue && maxStars.HasValue && minStars > maxStars)
				{
					var temp = minStars;
					minStars = maxStars;
					maxStars = temp;
					_logger.LogWarning("MinStars and MaxStars swapped due to invalid range");
				}

				var result = await _hotelRepository.SearchHotelsAdvancedAsync(
					searchTerm, minStars, maxStars, pageSize, cancellationToken);

				_logger.LogInformation("Advanced search completed - Found {HotelCount} hotels", result.Count);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred during advanced hotel search");
				throw;
			}
		}

		#region Private Validation Methods

		private void ValidateHotelCreateDto(HotelCreateDto hotelCreateDto)
		{
			if (hotelCreateDto == null)
			{
				throw new ArgumentNullException(nameof(hotelCreateDto), "Hotel create data cannot be null");
			}

			if (string.IsNullOrWhiteSpace(hotelCreateDto.Name))
			{
				throw new ArgumentException("Hotel name is required", nameof(hotelCreateDto.Name));
			}

			if (hotelCreateDto.Name.Length > 200)
			{
				throw new ArgumentException("Hotel name cannot exceed 200 characters", nameof(hotelCreateDto.Name));
			}

			if (hotelCreateDto.Stars.HasValue && (hotelCreateDto.Stars.Value < 0 || hotelCreateDto.Stars.Value > 5))
			{
				throw new ArgumentException("Hotel stars must be between 0 and 5", nameof(hotelCreateDto.Stars));
			}
		}

		private void ValidateHotelUpdateDto(HotelUpdateDto hotelUpdateDto)
		{
			if (hotelUpdateDto == null)
			{
				throw new ArgumentNullException(nameof(hotelUpdateDto), "Hotel update data cannot be null");
			}

			if (hotelUpdateDto.Id <= 0)
			{
				throw new ArgumentException("Valid hotel ID is required", nameof(hotelUpdateDto.Id));
			}

			if (string.IsNullOrWhiteSpace(hotelUpdateDto.Name))
			{
				throw new ArgumentException("Hotel name is required", nameof(hotelUpdateDto.Name));
			}

			if (hotelUpdateDto.Name.Length > 200)
			{
				throw new ArgumentException("Hotel name cannot exceed 200 characters", nameof(hotelUpdateDto.Name));
			}

			if (hotelUpdateDto.Stars < 0 || hotelUpdateDto.Stars > 5)
			{
				throw new ArgumentException("Hotel stars must be between 0 and 5", nameof(hotelUpdateDto.Stars));
			}
		}

		#endregion
	}
}