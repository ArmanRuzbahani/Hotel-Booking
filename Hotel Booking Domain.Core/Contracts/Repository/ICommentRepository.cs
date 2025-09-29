using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.DTO.HotelAddress;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Repository
{
	public interface ICommentRepository
	{
		Task<IReadOnlyCollection<HotelComments?>> GetAllCommentsAsync(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<HotelComments?>> GetCommentsByCustomerIdAsync(int customerId, CancellationToken cancellationToken);

		Task<IReadOnlyCollection<HotelComments?>> GetCommentsByHotelIdAsync(int hotelId, CancellationToken cancellationToken);

		Task<HotelComments> CreateHotelCommentAsync(HotelCommentsCreateDto hotelCommentCreateDto, CancellationToken cancellationToken);

		Task<HotelComments> UpdateCommentAsync(HotelCommentsUpdateDto hotelCommentUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken);

		Task<bool> CommentManagment(int id, CancellationToken cancellationToken); //id == Comment id 
	}
}
