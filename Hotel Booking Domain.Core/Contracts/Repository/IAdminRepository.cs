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
		Task<IReadOnlyCollection<Admin>> GetAllAdmins(CancellationToken cancellationToken);
		Task<Admin> GetAdminById(int Id, CancellationToken cancellationToken);
		Task<bool> DeleteAdmin(CancellationToken cancellationToken);
		Task<Admin> UpdateAdmin(AdminUpdateDto adminUpdateDto,CancellationToken cancellationToken);
	}
}
