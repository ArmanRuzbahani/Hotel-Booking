using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelComments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly ILogger<CommentService> _logger;

		public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
		{
			_commentRepository = commentRepository;
			_logger = logger;
		}

		public Task<HotelComments> CreateHotelCommentAsync(HotelCommentsCreateDto hotelCommentCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelComments?>> GetAllCommentsAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelComments?>> GetCommentsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<HotelComments?>> GetCommentsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<HotelComments> UpdateCommentAsync(HotelCommentsUpdateDto hotelCommentUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
