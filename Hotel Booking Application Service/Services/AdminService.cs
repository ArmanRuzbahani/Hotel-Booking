using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.Admin;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class AdminService : IAdminService
	{
		private readonly IAdminRepository _adminRepository;
		private readonly ILogger<AdminService> _logger;

		public AdminService(IAdminRepository adminRepository, ILogger<AdminService> logger)
		{
			_adminRepository = adminRepository;
			_logger = logger;
		}

		public Task<bool> DeleteAdminAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<AdminReadDto?> GetAdminByIdAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<AdminReadDto>> GetAllAdminsAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
