using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Admin;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class AdminService : IAdminService
	{
		private readonly IAdminRepository _adminRepository;
		private readonly ILogger<AdminService> _logger;

		public AdminService(IAdminRepository adminRepository, ILogger<AdminService> logger)
		{
			_adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> DeleteAdminAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Attempting to delete admin with ID: {AdminId}", id);

				
				if (id <= 0)
				{
					_logger.LogWarning("Invalid admin ID provided: {AdminId}", id);
					return false;
				}

				
				var existingAdmin = await _adminRepository.GetAdminByIdAsync(id, cancellationToken);
				if (existingAdmin == null)
				{
					_logger.LogWarning("Admin with ID {AdminId} not found for deletion", id);
					return false;
				}

				
				var allAdmins = await _adminRepository.GetAllAdminsAsync(cancellationToken);
				if (allAdmins.Count <= 1)
				{
					_logger.LogWarning("Cannot delete the last admin in the system");
					return false;
				}

				
				if (await HasActiveResponsibilitiesAsync(id, cancellationToken))
				{
					_logger.LogWarning("Cannot delete admin with active responsibilities: {AdminId}", id);
					return false;
				}

				
				var result = await _adminRepository.DeleteAdminAsync(id, cancellationToken);

				if (result)
				{
					_logger.LogInformation("Successfully deleted admin with ID: {AdminId}", id);
					await OnAdminDeletedAsync(id, existingAdmin, cancellationToken);
				}
				else
				{
					_logger.LogError("Failed to delete admin with ID: {AdminId}", id);
				}

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting admin with ID {AdminId}", id);
				throw;
			}
		}

		public async Task<AdminReadDto?> GetAdminByIdAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Retrieving admin with ID: {AdminId}", id);

				if (id <= 0)
				{
					_logger.LogWarning("Invalid admin ID provided: {AdminId}", id);
					return null;
				}

				var admin = await _adminRepository.GetAdminByIdAsync(id, cancellationToken);

				if (admin == null)
				{
					_logger.LogWarning("Admin with ID {AdminId} not found", id);
				}
				else
				{
					_logger.LogInformation("Successfully retrieved admin with ID: {AdminId}", id);
				}

				return admin;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving admin with ID {AdminId}", id);
				throw;
			}
		}

		public async Task<IReadOnlyCollection<AdminReadDto>> GetAllAdminsAsync(CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Retrieving all admins");

				var admins = await _adminRepository.GetAllAdminsAsync(cancellationToken);

				_logger.LogInformation("Successfully retrieved {AdminCount} admins", admins.Count);

				return admins;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all admins");
				throw;
			}
		}

		public async Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (adminUpdateDto == null)
				{
					_logger.LogWarning("Admin update DTO cannot be null");
					throw new ArgumentNullException(nameof(adminUpdateDto));
				}

				_logger.LogInformation("Updating admin with ID: {AdminId}", adminUpdateDto.Id);

				
				if (adminUpdateDto.Id <= 0)
				{
					throw new ArgumentException("Invalid admin ID", nameof(adminUpdateDto.Id));
				}

				
				var existingAdmin = await _adminRepository.GetAdminByIdAsync(adminUpdateDto.Id, cancellationToken);
				if (existingAdmin == null)
				{
					_logger.LogWarning("Admin with ID {AdminId} not found for update", adminUpdateDto.Id);
					throw new KeyNotFoundException($"Admin with ID {adminUpdateDto.Id} not found");
				}

				await ValidateAdminUpdateAsync(adminUpdateDto, cancellationToken);

				
				var updatedAdmin = await _adminRepository.UpdateAdminAsync(adminUpdateDto, cancellationToken);

				_logger.LogInformation("Successfully updated admin with ID: {AdminId}", adminUpdateDto.Id);

				return updatedAdmin;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating admin with ID {AdminId}", adminUpdateDto?.Id);
				throw;
			}
		}

		#region Private Helper Methods

		private async Task<bool> HasActiveResponsibilitiesAsync(int adminId, CancellationToken cancellationToken)
		{
			// Implement your business logic here
			// Example: Check if admin is assigned to active hotels, has pending approvals, etc.
			// This could involve calling other repositories or services

			// Placeholder implementation - extend based on your business rules
			_logger.LogDebug("Checking active responsibilities for admin ID: {AdminId}", adminId);

			// Return false for now - implement actual logic as needed
			return false;
		}

		private async Task OnAdminDeletedAsync(int adminId, AdminReadDto deletedAdmin, CancellationToken cancellationToken)
		{
			// Handle post-deletion side effects
			try
			{
				_logger.LogDebug("Processing side effects for deleted admin ID: {AdminId}", adminId);

				// Example side effects:
				// - Audit logging
				// - Cache invalidation
				// - Notifying other systems
				// - Cleaning up related data

				// Implement based on your requirements
				await Task.CompletedTask;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing side effects for deleted admin ID: {AdminId}", adminId);
				// Don't throw - this shouldn't affect the main deletion operation
			}
		}

		private async Task ValidateAdminUpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
		{
			// Add business validation logic for admin updates
			var errors = new List<string>();

			// Example validations:
			if (string.IsNullOrWhiteSpace(adminUpdateDto.Email))
			{
				errors.Add("Email is required");
			}
			else if (!IsValidEmail(adminUpdateDto.Email))
			{
				errors.Add("Invalid email format");
			}

			// Add more validations as needed based on your AdminUpdateDto properties

			if (errors.Any())
			{
				throw new InvalidOperationException($"Validation failed: {string.Join("; ", errors)}");
			}

			await Task.CompletedTask;
		}

		private bool IsValidEmail(string email)
		{
			// Simple email validation - consider using Regex or validation library
			return !string.IsNullOrWhiteSpace(email) &&
				   email.Contains('@') &&
				   email.Contains('.');
		}

		#endregion
	}
}