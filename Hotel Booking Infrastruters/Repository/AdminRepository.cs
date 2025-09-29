using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Admin;
using Hotel_Booking_Infrastruters.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Repository
{
	public class AdminRepository : IAdminRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<AdminRepository> _logger;

		public AdminRepository(AppDbContext appDbContext, ILogger<AdminRepository> logger)
		{
			_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> DeleteAdminAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var admin = await _appDbContext.admins.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

				if (admin == null)
				{
					_logger.LogWarning("Admin with ID {AdminId} not found for deletion.", id);
					return false;
				}

				_appDbContext.admins.Remove(admin);
				await _appDbContext.SaveChangesAsync(cancellationToken);

				_logger.LogInformation("Admin with ID {AdminId} deleted successfully.", id);
				return true;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error while deleting admin with ID {AdminId}", id);
				throw new InvalidOperationException("An error occurred while deleting the admin.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogWarning("Delete operation for admin with ID {AdminId} was canceled.", id);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error while deleting admin with ID {AdminId}", id);
				throw new InvalidOperationException("An unexpected error occurred while deleting the admin.", ex);
			}
		}

		public async Task<AdminReadDto?> GetAdminByIdAsync(int id, CancellationToken cancellationToken)
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
						IsPhoneNumberVerfied = x.IsPhoneNumberVerfied,
						Job = x.Job,
						MaritalStatus = x.MaritalStatus,
						Nationality = x.Nationality,
						PhoneNumber = x.PhoneNumber,
						Role = x.Role
					})
					.FirstOrDefaultAsync(cancellationToken);

				if (admin == null)
					_logger.LogWarning("Admin with ID {AdminId} not found.", id);
				else
					_logger.LogInformation("Admin with ID {AdminId} retrieved successfully.", id);

				return admin;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving admin with ID {AdminId}", id);
				throw new InvalidOperationException($"An error occurred while retrieving admin with ID {id}.", ex);
			}
		}

		public async Task<IReadOnlyCollection<AdminReadDto>> GetAllAdminsAsync(CancellationToken cancellationToken)
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
						IsPhoneNumberVerfied = x.IsPhoneNumberVerfied,
						Job = x.Job,
						MaritalStatus = x.MaritalStatus,
						Nationality = x.Nationality,
						PhoneNumber = x.PhoneNumber,
						Role = x.Role
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} admins.", admins.Count);
				return admins.AsReadOnly();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all admins.");
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

				var admin = await _appDbContext.admins.FirstOrDefaultAsync(x => x.Id == adminUpdateDto.Id, cancellationToken);

				if (admin == null)
				{
					_logger.LogWarning("Admin with ID {AdminId} not found for update.", adminUpdateDto.Id);
					throw new ArgumentException($"Admin with ID {adminUpdateDto.Id} not found.");
				}

				admin.Name = adminUpdateDto.Name ?? admin.Name;
				admin.LastName = adminUpdateDto.LastName ?? admin.LastName;
				admin.PhoneNumber = adminUpdateDto.PhoneNumber ?? admin.PhoneNumber;
				admin.CardId = adminUpdateDto.CardId ?? admin.CardId;
				admin.City = adminUpdateDto.City ?? admin.City;
				admin.DateOfBirth = adminUpdateDto.DateOfBirth ?? admin.DateOfBirth;
				admin.Education = adminUpdateDto.Education ?? admin.Education;
				admin.Email = adminUpdateDto.Email ?? admin.Email;
				admin.Gender = adminUpdateDto.Gender ?? admin.Gender;
				admin.Job = adminUpdateDto.Job ?? admin.Job;
				admin.MaritalStatus = adminUpdateDto.MaritalStatus ?? admin.MaritalStatus;

				await _appDbContext.SaveChangesAsync(cancellationToken);

				_logger.LogInformation("Admin with ID {AdminId} updated successfully.", admin.Id);
				return admin;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating admin with ID {AdminId}", adminUpdateDto?.Id);
				throw new InvalidOperationException($"An error occurred while updating admin with ID {adminUpdateDto?.Id}.", ex);
			}
		}
	}
}
