using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelManager;
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
	public  class HotelManagerRepository : IHotelManagerRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<HotelManagerRepository> _logger;	

		public HotelManagerRepository(AppDbContext appDbContext, ILogger<HotelManagerRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}
		public async Task<HotelManager> CreateHotelManagerAsync(HotelManagerCreateDto hotelManagerCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var newManager = new HotelManager
				{
					Name = hotelManagerCreateDto.Name,
					LastName = hotelManagerCreateDto.LastName,
					Email = hotelManagerCreateDto.Email,
					DateOfBirth = hotelManagerCreateDto.DateOfBirth,
					PhoneNumber = hotelManagerCreateDto.PhoneNumber,
					CardId = hotelManagerCreateDto.CardId,
					Job = hotelManagerCreateDto.Job,
					MaritalStatus = hotelManagerCreateDto.MaritalStatus,
					Gender = hotelManagerCreateDto.Gender,
					Education = hotelManagerCreateDto.Education,
					Nationality = hotelManagerCreateDto.Nationality,
					City = hotelManagerCreateDto.city,
					Role = hotelManagerCreateDto.Role
				};

				await _appDbContext.hotelManager.AddAsync(newManager, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return newManager;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel manager");
				throw;
			}
		}


		public async Task<bool> DeleteHotelManagerAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var manager = await _appDbContext.hotelManager
					.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

				if (manager == null)
					return false;

				_appDbContext.hotelManager.Remove(manager);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel manager");
				throw;
			}
		}


		public async Task<IReadOnlyCollection<HotelManager>> GetAllHotelManagersAsync(CancellationToken cancellationToken)
		{
			try
			{
				var managers = await _appDbContext.hotelManager
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return managers;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotel managers");
				throw;
			}
		}

		public async Task<HotelManager?> GetHotelManagerByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				var manager = await _appDbContext.hotels
					.Where(h => h.Id == hotelId)
					.Select(h => h.HotelManager)
					.AsNoTracking()
					.FirstOrDefaultAsync(cancellationToken);

				return manager;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving hotel manager by hotel ID");
				throw;
			}
		}


		public async Task<HotelManager> UpdateHotelManagerAsync(HotelManagerUpdateDto hotelManagerUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var manager = await _appDbContext.hotelManager
					.FirstOrDefaultAsync(m => m.Id == hotelManagerUpdateDto.Id, cancellationToken);

				if (manager == null)
					return null;

				manager.Name = hotelManagerUpdateDto.Name;
				manager.LastName = hotelManagerUpdateDto.LastName;
				manager.Email = hotelManagerUpdateDto.Email;
				manager.DateOfBirth = hotelManagerUpdateDto.DateOfBirth;
				manager.PhoneNumber = hotelManagerUpdateDto.PhoneNumber;
				manager.CardId = hotelManagerUpdateDto.CardId;
				manager.Job = hotelManagerUpdateDto.Job;
				manager.MaritalStatus = hotelManagerUpdateDto.MaritalStatus;
				manager.Gender = hotelManagerUpdateDto.Gender;
				manager.Education = hotelManagerUpdateDto.Education;
				manager.Nationality = hotelManagerUpdateDto.Nationality;
				manager.City = hotelManagerUpdateDto.City;
				
				_appDbContext.hotelManager.Update(manager);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return manager;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel manager");
				throw;
			}
		}
	}
}
