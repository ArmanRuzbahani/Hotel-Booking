using Entitys_Hotel.Models;
using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelComments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly ILogger<CommentService> _logger;

		public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
		{
			if (commentRepository == null)
			{
				_logger?.LogError("ریپازیتوری نظرات نمی‌تواند خالی باشد.");
				throw new ArgumentNullException(nameof(commentRepository), "ریپازیتوری نظرات نمی‌تواند خالی باشد.");
			}
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger), "لاگر نمی‌تواند خالی باشد.");
			}

			_commentRepository = commentRepository;
			_logger = logger;
		}

		public async Task<bool> CommentManagment(int id, CancellationToken cancellationToken)
		{
			if (id <= 0)
			{
				_logger.LogWarning("Invalid CommentId: {CommentId}", id);
				return false;
			}

			var result = await _commentRepository.CommentManagment(id, cancellationToken);
			return result;
		}


		public async Task<HotelComments> CreateHotelCommentAsync(HotelCommentsCreateDto hotelCommentCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelCommentCreateDto == null)
				{
					_logger.LogWarning("تلاش برای ایجاد نظر با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(hotelCommentCreateDto), "داده‌های ورودی نظر نمی‌تواند خالی باشد.");
				}

				if (hotelCommentCreateDto.CustomerId <= 0)
				{
					_logger.LogWarning("شناسه مشتری نامعتبر: {CustomerId}. باید بیشتر از صفر باشد.", hotelCommentCreateDto.CustomerId);
					throw new ArgumentException("شناسه مشتری باید بیشتر از صفر باشد.");
				}

				if (hotelCommentCreateDto.HotelId <= 0)
				{
					_logger.LogWarning("شناسه هتل نامعتبر: {HotelId}. باید بیشتر از صفر باشد.", hotelCommentCreateDto.HotelId);
					throw new ArgumentException("شناسه هتل باید بیشتر از صفر باشد.");
				}

				if (string.IsNullOrWhiteSpace(hotelCommentCreateDto.Content))
				{
					_logger.LogWarning("محتوای نظر نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("محتوای نظر نمی‌تواند خالی باشد.");
				}

				if (hotelCommentCreateDto.Rating < 1 || hotelCommentCreateDto.Rating > 5)
				{
					_logger.LogWarning("امتیاز نامعتبر: {Rating}. باید بین 1 تا 5 باشد.", hotelCommentCreateDto.Rating);
					throw new ArgumentException("امتیاز باید بین 1 تا 5 باشد.");
				}

				if (hotelCommentCreateDto.CreatedAt == default)
				{
					_logger.LogWarning("تاریخ ایجاد نظر نامعتبر است: {CreatedAt}.", hotelCommentCreateDto.CreatedAt);
					throw new ArgumentException("تاریخ ایجاد نظر باید معتبر باشد.");
				}

				var comment = await _commentRepository.CreateHotelCommentAsync(hotelCommentCreateDto, cancellationToken);
				_logger.LogInformation("نظر با شناسه {CommentId} برای هتل با شناسه {HotelId} و مشتری با شناسه {CustomerId} با موفقیت ایجاد شد.",
					comment.Id, comment.HotelId, comment.CustomerId);
				return comment;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد نظر برای هتل با شناسه {HotelId} و مشتری با شناسه {CustomerId}.",
					hotelCommentCreateDto?.HotelId ?? 0, hotelCommentCreateDto?.CustomerId ?? 0);
				throw new InvalidOperationException("خطایی در هنگام ایجاد نظر رخ داد.", ex);
			}
		}

		public async Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken)
		{
			try
			{
				if (id <= 0)
				{
					_logger.LogWarning("شناسه نظر نامعتبر: {CommentId}. باید بیشتر از صفر باشد.", id);
					throw new ArgumentException("شناسه نظر باید بیشتر از صفر باشد.");
				}

				var result = await _commentRepository.DeleteCommentAsync(id, cancellationToken);
				if (result)
				{
					_logger.LogInformation("نظر با شناسه {CommentId} با موفقیت حذف شد.", id);
				}
				else
				{
					_logger.LogWarning("نظر با شناسه {CommentId} برای حذف یافت نشد.", id);
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف نظر با شناسه {CommentId}.", id);
				throw new InvalidOperationException("خطایی در هنگام حذف نظر رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<HotelComments?>> GetAllCommentsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var comments = await _commentRepository.GetAllCommentsAsync(cancellationToken);
				if (comments.Count > 0)
				{
					_logger.LogInformation("{Count} نظر با موفقیت دریافت شد.", comments.Count);
				}
				else
				{
					_logger.LogInformation("هیچ نظری یافت نشد.");
				}
				return comments;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت همه نظرات.");
				throw new InvalidOperationException("خطایی در هنگام دریافت نظرات رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<HotelComments?>> GetCommentsByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				if (customerId <= 0)
				{
					_logger.LogWarning("شناسه مشتری نامعتبر: {CustomerId}. باید بیشتر از صفر باشد.", customerId);
					throw new ArgumentException("شناسه مشتری باید بیشتر از صفر باشد.");
				}

				var comments = await _commentRepository.GetCommentsByCustomerIdAsync(customerId, cancellationToken);
				if (comments.Count > 0)
				{
					_logger.LogInformation("{Count} نظر برای مشتری با شناسه {CustomerId} با موفقیت دریافت شد.",
						comments.Count, customerId);
				}
				else
				{
					_logger.LogInformation("هیچ نظری برای مشتری با شناسه {CustomerId} یافت نشد.", customerId);
				}
				return comments;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت نظرات برای مشتری با شناسه {CustomerId}.", customerId);
				throw new InvalidOperationException("خطایی در هنگام دریافت نظرات مشتری رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<HotelComments?>> GetCommentsByHotelIdAsync(int hotelId, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelId <= 0)
				{
					_logger.LogWarning("شناسه هتل نامعتبر: {HotelId}. باید بیشتر از صفر باشد.", hotelId);
					throw new ArgumentException("شناسه هتل باید بیشتر از صفر باشد.");
				}

				var comments = await _commentRepository.GetCommentsByHotelIdAsync(hotelId, cancellationToken);
				if (comments.Count > 0)
				{
					_logger.LogInformation("{Count} نظر برای هتل با شناسه {HotelId} با موفقیت دریافت شد.",
						comments.Count, hotelId);
				}
				else
				{
					_logger.LogInformation("هیچ نظری برای هتل با شناسه {HotelId} یافت نشد.", hotelId);
				}
				return comments;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت نظرات برای هتل با شناسه {HotelId}.", hotelId);
				throw new InvalidOperationException("خطایی در هنگام دریافت نظرات هتل رخ داد.", ex);
			}
		}

		public async Task<HotelComments> UpdateCommentAsync(HotelCommentsUpdateDto hotelCommentUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (hotelCommentUpdateDto == null)
				{
					_logger.LogWarning("تلاش برای به‌روزرسانی نظر با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(hotelCommentUpdateDto), "داده‌های ورودی به‌روزرسانی نظر نمی‌تواند خالی باشد.");
				}

				if (hotelCommentUpdateDto.Id <= 0)
				{
					_logger.LogWarning("شناسه نظر نامعتبر: {CommentId}. باید بیشتر از صفر باشد.", hotelCommentUpdateDto.Id);
					throw new ArgumentException("شناسه نظر باید بیشتر از صفر باشد.");
				}

				if (string.IsNullOrWhiteSpace(hotelCommentUpdateDto.Content))
				{
					_logger.LogWarning("محتوای نظر نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("محتوای نظر نمی‌تواند خالی باشد.");
				}

				if (hotelCommentUpdateDto.Rating < 1 || hotelCommentUpdateDto.Rating > 5)
				{
					_logger.LogWarning("امتیاز نامعتبر: {Rating}. باید بین 1 تا 5 باشد.", hotelCommentUpdateDto.Rating);
					throw new ArgumentException("امتیاز باید بین 1 تا 5 باشد.");
				}

				var comment = await _commentRepository.UpdateCommentAsync(hotelCommentUpdateDto, cancellationToken);
				if (comment == null)
				{
					_logger.LogWarning("نظر با شناسه {CommentId} برای به‌روزرسانی یافت نشد.", hotelCommentUpdateDto.Id);
					throw new KeyNotFoundException($"نظر با شناسه {hotelCommentUpdateDto.Id} یافت نشد.");
				}

				_logger.LogInformation("نظر با شناسه {CommentId} با موفقیت به‌روزرسانی شد.", comment.Id);
				return comment;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی نظر با شناسه {CommentId}.", hotelCommentUpdateDto?.Id ?? 0);
				throw new InvalidOperationException("خطایی در هنگام به‌روزرسانی نظر رخ داد.", ex);
			}
		}
	}
}