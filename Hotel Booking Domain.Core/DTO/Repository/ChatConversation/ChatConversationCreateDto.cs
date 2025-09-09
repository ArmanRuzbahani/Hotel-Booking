using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.ChatConversation
{
	public class ChatConversationCreateDto
	{
		public string Title { get; set; } // خلاصه اولین سوال کاربر
		public DateTime StartedAt { get; set; } = DateTime.Now;
		public DateTime? EndedAt { get; set; }
		// تمام سوالات و پاسخ‌ها در یک فیلد ذخیره می‌شوند
		public string ConversationData { get; set; }
		public int CustomerId { get; set; }
	}
}
