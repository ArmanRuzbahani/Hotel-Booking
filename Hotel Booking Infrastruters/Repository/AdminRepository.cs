using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Admin;
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
	public class AdminRepository : IAdminRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<AdminRepository> _logger;

		public AdminRepository(AppDbContext appDbContext,ILogger<AdminRepository> logger)
		{
			appDbContext = _appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> DeleteAdminAsync(int id, CancellationToken cancellationToken = default)
		{
			try
			{
              var admin = await _appDbContext.admins.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

				if (admin == null)
				{
					throw new ArgumentException($"Admin with ID {id} not found.");
				}

				_appDbContext.admins.Remove(admin);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (ArgumentException ex)
			{
				throw; 
			}
			catch (DbUpdateException ex)
			{
				throw new InvalidOperationException("An error occurred while deleting the admin.", ex);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An unexpected error occurred while deleting the admin.", ex);
			}
		}

		public async Task<AdminReadDto?> GetAdminByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			try
			{
				var admin = await _appDbContext.admins
					.AsNoTracking()
					.Where(x => x.Id == id)
					.Select(x => new AdminReadDto
					{
						Id = x.Id,
						Name = x.Name,
						LastName = x.LastName,
						IsActive = x.IsActive,
						UserCreateAt = x.UserCreateAt,
						CardId = x.CardId,
						City = x.City,
						DateOfBirth = x.DateOfBirth,
						Education = x.Education,
						Email = x.Email,
						Gender = x.Gender,
						IsEmailVerfied = x.IsEmailVerfied,
						IsPhoneNumberVerfied = x.IsPhoneNumberVerfied,
						Job = x.Job,
						MaritalStatus = x.MaritalStatus,
						Nationality = x.Nationality,
						PhoneNumber = x.PhoneNumber,
						Role = x.Role
					})
					.FirstOrDefaultAsync(cancellationToken);

				return admin;
			}
			catch (Exception ex)
			{
			
				throw new InvalidOperationException($"An error occurred while retrieving admin with ID {id}.", ex);
			}
		}

		public async Task<IReadOnlyCollection<AdminReadDto>> GetAllAdminsAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var admins = await _appDbContext.admins
					.AsNoTracking()
					.Select(x => new AdminReadDto
					{
						Id = x.Id,
						Name = x.Name,
						LastName = x.LastName,
						IsActive = x.IsActive,
						UserCreateAt = x.UserCreateAt,
						CardId = x.CardId,
						City = x.City,
						DateOfBirth = x.DateOfBirth,
						Education = x.Education,
						Email = x.Email,
						Gender = x.Gender,
						IsEmailVerfied = x.IsEmailVerfied,
						IsPhoneNumberVerfied = x.IsPhoneNumberVerfied,
						Job = x.Job,
						MaritalStatus = x.MaritalStatus,
						Nationality = x.Nationality,
						PhoneNumber = x.PhoneNumber,
						Role = x.Role
					})
					.ToListAsync(cancellationToken);

				return admins.AsReadOnly();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while retrieving all admins.", ex);
			}
		}

		public async Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (adminUpdateDto == null)
				{
					throw new ArgumentNullException(nameof(adminUpdateDto), "Admin update data cannot be null.");
				}

				var adminsforUpdate = await _appDbContext.admins
					.FirstOrDefaultAsync(x => x.Id == adminUpdateDto.Id, cancellationToken);

				if (adminsforUpdate == null)
				{
					throw new ArgumentException($"Admin with ID {adminUpdateDto.Id} not found.");
				}

				adminsforUpdate.Name = adminUpdateDto.Name ?? adminsforUpdate.Name;
				adminsforUpdate.LastName = adminUpdateDto.LastName ?? adminsforUpdate.LastName;
				adminsforUpdate.PhoneNumber = adminUpdateDto.PhoneNumber ?? adminsforUpdate.PhoneNumber;
				adminsforUpdate.CardId = adminUpdateDto.CardId ?? adminsforUpdate.CardId;
				adminsforUpdate.City = adminUpdateDto.City ?? adminsforUpdate.City;
				adminsforUpdate.DateOfBirth = adminUpdateDto.DateOfBirth ?? adminsforUpdate.DateOfBirth;
				adminsforUpdate.Education = adminUpdateDto.Education ?? adminsforUpdate.Education;
				adminsforUpdate.Email = adminUpdateDto.Email ?? adminsforUpdate.Email;
				adminsforUpdate.Gender = adminUpdateDto.Gender ?? adminsforUpdate.Gender;
				adminsforUpdate.Job = adminUpdateDto.Job ?? adminsforUpdate.Job;
				adminsforUpdate.MaritalStatus = adminUpdateDto.MaritalStatus ?? adminsforUpdate.MaritalStatus;
				
				await _appDbContext.SaveChangesAsync(cancellationToken);

				return adminsforUpdate;
			}
			catch (ArgumentNullException ex)
			{
				throw;
			}
			catch (ArgumentException ex)
			{
				throw;
			}
			catch (DbUpdateException ex)
			{
				throw new InvalidOperationException($"An error occurred while updating admin with ID {adminUpdateDto?.Id}.", ex);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"An unexpected error occurred while updating admin with ID {adminUpdateDto?.Id}.", ex);
			}
		}
	}
}
