using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class ChatConversationService : IChatConversationService
	{
		private readonly IChatConversationRepository _chatConversationRepository;
		private readonly ILogger<ChatConversationService> _logger;

		public ChatConversationService(IChatConversationRepository chatConversationRepository, ILogger<ChatConversationService> logger)
		{
			if (chatConversationRepository == null)
			{
				_logger?.LogError("ریپازیتوری مکالمه نمی‌تواند خالی باشد.");
				throw new ArgumentNullException(nameof(chatConversationRepository), "ریپازیتوری مکالمه نمی‌تواند خالی باشد.");
			}
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger), "لاگر نمی‌تواند خالی باشد.");
			}

			_chatConversationRepository = chatConversationRepository;
			_logger = logger;
		}

		public async Task<ChatConversation> CreateChatConversation(ChatConversationCreateDto chatConversationCreateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (chatConversationCreateDto == null)
				{
					_logger.LogWarning("تلاش برای ایجاد مکالمه با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(chatConversationCreateDto), "داده‌های ورودی مکالمه نمی‌تواند خالی باشد.");
				}

				if (chatConversationCreateDto.CustomerId <= 0)
				{
					_logger.LogWarning("شناسه مشتری نامعتبر: {CustomerId}. باید بیشتر از صفر باشد.", chatConversationCreateDto.CustomerId);
					throw new ArgumentException("شناسه مشتری باید بیشتر از صفر باشد.");
				}

				if (string.IsNullOrWhiteSpace(chatConversationCreateDto.Title))
				{
					_logger.LogWarning("عنوان مکالمه نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("عنوان مکالمه نمی‌تواند خالی باشد.");
				}

				if (chatConversationCreateDto.StartedAt == default)
				{
					_logger.LogWarning("تاریخ شروع مکالمه نامعتبر است: {StartedAt}.", chatConversationCreateDto.StartedAt);
					throw new ArgumentException("تاریخ شروع مکالمه باید معتبر باشد.");
				}

				var chatConversation = await _chatConversationRepository.CreateChatConversation(chatConversationCreateDto, cancellationToken);
				_logger.LogInformation("مکالمه با شناسه {ChatId} برای مشتری با شناسه {CustomerId} با موفقیت ایجاد شد.",
					chatConversation.Id, chatConversation.CustomerId);
				return chatConversation;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ایجاد مکالمه برای مشتری با شناسه {CustomerId}.", chatConversationCreateDto?.CustomerId ?? 0);
				throw new InvalidOperationException("خطایی در هنگام ایجاد مکالمه رخ داد.", ex);
			}
		}

		public async Task<bool> DeleteChatConversation(int chatId, CancellationToken cancellationToken)
		{
			try
			{
				if (chatId <= 0)
				{
					_logger.LogWarning("شناسه مکالمه نامعتبر: {ChatId}. باید بیشتر از صفر باشد.", chatId);
					throw new ArgumentException("شناسه مکالمه باید بیشتر از صفر باشد.");
				}

				var result = await _chatConversationRepository.DeleteChatConversation(chatId, cancellationToken);
				if (result)
				{
					_logger.LogInformation("مکالمه با شناسه {ChatId} با موفقیت حذف شد.", chatId);
				}
				else
				{
					_logger.LogWarning("مکالمه با شناسه {ChatId} برای حذف یافت نشد.", chatId);
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در حذف مکالمه با شناسه {ChatId}.", chatId);
				throw new InvalidOperationException("خطایی در هنگام حذف مکالمه رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversations(CancellationToken cancellationToken)
		{
			try
			{
				var conversations = await _chatConversationRepository.GetAllTheConversations(cancellationToken);
				if (conversations.Count > 0)
				{
					_logger.LogInformation("{Count} مکالمه با موفقیت دریافت شد.", conversations.Count);
				}
				else
				{
					_logger.LogInformation("هیچ مکالمه‌ای یافت نشد.");
				}
				return conversations;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت همه مکالمات.");
				throw new InvalidOperationException("خطایی در هنگام دریافت مکالمات رخ داد.", ex);
			}
		}

		public async Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversationsByCustomerId(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				if (customerId <= 0)
				{
					_logger.LogWarning("شناسه مشتری نامعتبر: {CustomerId}. باید بیشتر از صفر باشد.", customerId);
					throw new ArgumentException("شناسه مشتری باید بیشتر از صفر باشد.");
				}

				var conversations = await _chatConversationRepository.GetAllTheConversationsByCustomerId(customerId, cancellationToken);
				if (conversations.Count > 0)
				{
					_logger.LogInformation("{Count} مکالمه برای مشتری با شناسه {CustomerId} با موفقیت دریافت شد.",
						conversations.Count, customerId);
				}
				else
				{
					_logger.LogInformation("هیچ مکالمه‌ای برای مشتری با شناسه {CustomerId} یافت نشد.", customerId);
				}
				return conversations;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در دریافت مکالمات برای مشتری با شناسه {CustomerId}.", customerId);
				throw new InvalidOperationException("خطایی در هنگام دریافت مکالمات مشتری رخ داد.", ex);
			}
		}

		public async Task<ChatConversation> UpdateChatConversation(ChatConversationUpdateDto chatConversationUpdateDto, CancellationToken cancellationToken)
		{
			try
			{
				if (chatConversationUpdateDto == null)
				{
					_logger.LogWarning("تلاش برای به‌روزرسانی مکالمه با داده‌های ورودی خالی.");
					throw new ArgumentNullException(nameof(chatConversationUpdateDto), "داده‌های ورودی به‌روزرسانی مکالمه نمی‌تواند خالی باشد.");
				}

				if (chatConversationUpdateDto.Id <= 0)
				{
					_logger.LogWarning("شناسه مکالمه نامعتبر: {ChatId}. باید بیشتر از صفر باشد.", chatConversationUpdateDto.Id);
					throw new ArgumentException("شناسه مکالمه باید بیشتر از صفر باشد.");
				}

				if (chatConversationUpdateDto.Title != null && string.IsNullOrWhiteSpace(chatConversationUpdateDto.Title))
				{
					_logger.LogWarning("عنوان مکالمه نمی‌تواند خالی یا نامعتبر باشد.");
					throw new ArgumentException("عنوان مکالمه نمی‌تواند خالی باشد.");
				}

				var chatConversation = await _chatConversationRepository.UpdateChatConversation(chatConversationUpdateDto, cancellationToken);
				_logger.LogInformation("مکالمه با شناسه {ChatId} با موفقیت به‌روزرسانی شد.", chatConversation.Id);
				return chatConversation;
			}
			catch (KeyNotFoundException ex)
			{
				_logger.LogWarning("مکالمه با شناسه {ChatId} برای به‌روزرسانی یافت نشد.", chatConversationUpdateDto?.Id ?? 0);
				throw new InvalidOperationException($"مکالمه با شناسه {chatConversationUpdateDto?.Id ?? 0} یافت نشد.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در به‌روزرسانی مکالمه با شناسه {ChatId}.", chatConversationUpdateDto?.Id ?? 0);
				throw new InvalidOperationException("خطایی در هنگام به‌روزرسانی مکالمه رخ داد.", ex);
			}
		}
	}
}