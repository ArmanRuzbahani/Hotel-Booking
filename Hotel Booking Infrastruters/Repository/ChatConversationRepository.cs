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

		public async Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversations(CancellationToken cancellationToken)
		{
			try
			{
				var ChatForAdmin = await _dbcontext.chatConversations
					.AsNoTracking()
					.Select(c => new ChatConversationReadDto
					{
						Id = c.Id,
						Title = c.Title,
						StartedAt = c.StartedAt,
						EndedAt = c.EndedAt,
						ConversationData = c.ConversationData,
						CustomerId = c.CustomerId
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} chat conversations.", ChatForAdmin.Count);

				return ChatForAdmin.AsReadOnly();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching chat conversations.");
				throw new InvalidOperationException("Database error occurred while fetching chat conversations.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching chat conversations was cancelled.");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching chat conversations.");
				throw new InvalidOperationException("Unexpected error occurred while fetching chat conversations.", ex);
			}
		}


		public async Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversationsByCustomerId(int customerId, CancellationToken cancellationToken)
		{
			try
			{
				var customerChats = await _dbcontext.chatConversations
					.AsNoTracking()
					.Where(c => c.CustomerId == customerId)
					.Select(c => new ChatConversationReadDto
					{
						Id = c.Id,
						Title = c.Title,
						StartedAt = c.StartedAt,
						EndedAt = c.EndedAt,
						ConversationData = c.ConversationData,
						CustomerId = c.CustomerId
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Retrieved {Count} chat conversations for CustomerId {CustomerId}.", customerChats.Count, customerId);

				return customerChats.AsReadOnly();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while fetching chat conversations for CustomerId {CustomerId}.", customerId);
				throw new InvalidOperationException("Database error occurred while fetching chat conversations.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Fetching chat conversations for CustomerId {CustomerId} was cancelled.", customerId);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while fetching chat conversations for CustomerId {CustomerId}.", customerId);
				throw new InvalidOperationException("Unexpected error occurred while fetching chat conversations.", ex);
			}
		}


		public async Task<ChatConversation> UpdateChatConversation(ChatConversationUpdateDto chatConversationUpdateDto, CancellationToken cancellationToken)
		{
			if (chatConversationUpdateDto == null)
				throw new ArgumentNullException(nameof(chatConversationUpdateDto));

			try
			{
				var chat = await _dbcontext.chatConversations
					.FirstOrDefaultAsync(c => c.Id == chatConversationUpdateDto.Id, cancellationToken);

				if (chat == null)
				{
					_logger.LogWarning("ChatConversation with ID {ChatId} not found for update.", chatConversationUpdateDto.Id);
					throw new KeyNotFoundException($"ChatConversation with ID {chatConversationUpdateDto.Id} not found.");
				}

				chat.Title = chatConversationUpdateDto.Title ?? chat.Title;
	
				await _dbcontext.SaveChangesAsync(cancellationToken);

				_logger.LogInformation("ChatConversation with ID {ChatId} updated successfully.", chat.Id);

				return chat;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred while updating ChatConversation with ID {ChatId}", chatConversationUpdateDto.Id);
				throw new InvalidOperationException("Database error occurred while updating the chat conversation.", ex);
			}
			catch (OperationCanceledException)
			{
				_logger.LogInformation("Update operation for ChatConversationId {ChatId} was cancelled.", chatConversationUpdateDto.Id);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred while updating ChatConversation with ID {ChatId}", chatConversationUpdateDto.Id);
				throw new InvalidOperationException("Unexpected error occurred while updating the chat conversation.", ex);
			}
		}

	}
}
