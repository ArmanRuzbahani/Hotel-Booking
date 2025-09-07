using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface IAdminRepository
	{
		Task<IReadOnlyCollection<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken);

		Task<Admin?> GetAdminByIdAsync(int id, CancellationToken cancellationToken);

		Task<bool> DeleteAdminAsync(int id, CancellationToken cancellationToken);

		Task<Admin> UpdateAdminAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken);

	}
}
