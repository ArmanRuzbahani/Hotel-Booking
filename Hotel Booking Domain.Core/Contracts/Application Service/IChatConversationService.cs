using Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation;
using Hotel_Booking_Domain.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Contracts.Application_Service
{
	public interface IChatConversationService
	{
		Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversations(CancellationToken cancellationToken);

		Task<IReadOnlyCollection<ChatConversationReadDto?>> GetAllTheConversationsByCustomerId(int customerId, CancellationToken cancellationToken);

		Task<ChatConversation> CreateChatConversation(ChatConversationCreateDto chatConversationCreateDto, CancellationToken cancellationToken);

		Task<ChatConversation> UpdateChatConversation(ChatConversationUpdateDto chatConversationUpdateDto, CancellationToken cancellationToken);

		Task<bool> DeleteChatConversation(int chatId, CancellationToken cancellationToken);
	}
}
