using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation
{
	public class ChatConversationReadDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime StartedAt { get; set; }
		public DateTime? EndedAt { get; set; }
		public string ConversationData { get; set; }
		public int CustomerId { get; set; }
	}
}
