using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation;
using Hotel_Booking_Domain.Core.Entitys;
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
	public class ChatConversationRepository : IChatConversationRepository
	{
		private readonly AppDbContext _dbcontext;
		private readonly ILogger<IChatConversationRepository> _logger;

		public ChatConversationRepository(AppDbContext dbcontext, ILogger<IChatConversationRepository> logger)
		{
			_dbcontext = dbcontext;
			_logger = logger;
		}

		public async Task<ChatConversation> CreateChatConversation(ChatConversationCreateDto chatConversationCreateDto, CancellationToken cancellationToken)
		{
			if (chatConversationCreateDto == null)
				throw new ArgumentNullException(nameof(chatConversationCreateDto));

			var newChat = new ChatConversation
			{
				Title = chatConversationCreateDto.Title,
				ConversationData = chatConversationCreateDto.ConversationData,
				CustomerId = chatConversationCreateDto.CustomerId,
				StartedAt = chatConversationCreateDto.StartedAt
			};

			try
			{
				await _dbcontext.chatConversations.AddAsync(newChat, cancellationToken);
				await _dbcontext.SaveChangesAsync(cancellationToken);

				_logger.LogInformation(
					"ChatConversation {ChatId} created successfully for CustomerId {CustomerId}",
					newChat.Id, newChat.CustomerId);

				return newChat;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(
					ex,
					"Database error occurred while creating ChatConversation for CustomerId {CustomerId}",
					newChat.CustomerId);
				throw new InvalidOperationException("Database error occurred while creating the chat conversation.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Chat creation was cancelled for CustomerId {CustomerId}", newChat.CustomerId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Unexpected error occurred while creating ChatConversation for CustomerId {CustomerId}",
					newChat.CustomerId);
				throw new InvalidOperationException("Unexpected error occurred while creating the chat conversation.", ex);
			}
		}

		public async Task<bool> DeleteChatConversation(int chatId, CancellationToken cancellationToken)
		{
			try
			{
				var ChatForBin = await _dbcontext.chatConversations
					.FirstOrDefaultAsync(c => c.Id == chatId, cancellationToken);

				if (ChatForBin == null)
				{
					_logger.LogWarning("ChatConversation with Id {ChatId} not found for deletion", chatId);
					return false;
				}

				_dbcontext.chatConversations.Remove(ChatForBin);
				var changesSaved = await _dbcontext.SaveChangesAsync(cancellationToken) > 0;

				if (changesSaved)
					_logger.LogInformation("ChatConversation with Id {ChatId} deleted successfully", chatId);

				return changesSaved;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while deleting ChatConversation with Id {ChatId}", chatId);
				throw new InvalidOperationException("Database error occurred while deleting the chat conversation.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Deletion of ChatConversation with Id {ChatId} was cancelled", chatId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while deleting ChatConversation with Id {ChatId}", chatId);
				throw new InvalidOperationException("Unexpected error occurred while deleting the chat conversation.", ex);
			}
		}

		public Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversations(CancellationToken cancellationToken)
		{
			try
			{
				var 
			}
		}

		public Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversationsByCustomerId(int customerId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ChatConversation> UpdateChatConversation(ChatConversationUpdateDto chatConversationUpdateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
