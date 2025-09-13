using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelComments;
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
	public class CommentRepository : ICommentRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<CommentRepository> _logger;

		public CommentRepository(AppDbContext appDbContext, ILogger<CommentRepository> logger)
		{
			_appDbContext = appDbContext;
			_logger = logger;
		}

		public async Task<HotelComments> CreateHotelCommentAsync(HotelCommentsCreateDto hotelCommentCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				var newComment = new HotelComments
				{
					CustomerId = hotelCommentCreateDto.CustomerId,
					HotelId = hotelCommentCreateDto.HotelId,
					Content = hotelCommentCreateDto.Content,
					Rating = hotelCommentCreateDto.Rating,
					CreatedDateAt = hotelCommentCreateDto.CreatedAt
				};

				await _appDbContext.hotelComments.AddAsync(newComment, cancellationToken);
				await _appDbContext.SaveChangesAsync(cancellationToken);
				return newComment;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating hotel comment");
				throw;
			}
		}

		public async Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				var comment = await _appDbContext.hotelComments.FindAsync(new object[] { id }, cancellationToken);
				if (comment == null)
				{
					return false;
				}

				_appDbContext.hotelComments.Remove(comment);
				await _appDbContext.SaveChangesAsync(cancellationToken);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while deleting hotel comment");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelComments>> GetAllCommentsAsync(CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelComments
					.AsNoTracking()
					.ToListAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving all hotel comments");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelComments>> GetCommentsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelComments
					.AsNoTracking()
					.Where(c => c.CustomerId == customerId)
					.ToListAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving comments by customer ID");
				throw;
			}
		}

		public async Task<IReadOnlyCollection<HotelComments>> GetCommentsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				return await _appDbContext.hotelComments
					.AsNoTracking()
					.Where(c => c.HotelId == hotelId)
					.ToListAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while retrieving comments by hotel ID");
				throw;
			}
		}

		public async Task<HotelComments> UpdateCommentAsync(HotelCommentsUpdateDto hotelCommentUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				var comment = await _appDbContext.hotelComments
					.FirstOrDefaultAsync(c => c.Id == hotelCommentUpdateDto.Id, cancellationToken);

				if (comment == null)
				{
					return null;
				}

				comment.Content = hotelCommentUpdateDto.Content;
				comment.Rating = hotelCommentUpdateDto.Rating;

				await _appDbContext.SaveChangesAsync(cancellationToken);
				return comment;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating hotel comment");
				throw;
			}
		}
	}
}