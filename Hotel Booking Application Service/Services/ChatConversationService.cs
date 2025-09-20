using Hotel_Booking_Domain.Core.Contracts.Application_Service;
using Hotel_Booking_Domain.Core.Contracts.Repository;
using Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation;
using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Application_Service.Services
{
	public class ChatConversationService : IChatConversationService
	{
		private readonly IChatConversationRepository _chatConversationRepository;
		private readonly ILogger<ChatConversationService> _logger;

		public ChatConversationService(IChatConversationRepository chatConversationRepository, ILogger<ChatConversationService> logger)
		{
			_chatConversationRepository = chatConversationRepository;
			_logger = logger;
		}

		public Task<ChatConversation> CreateChatConversation(ChatConversationCreateDto chatConversationCreateDto, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteChatConversation(int chatId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversations(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
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
