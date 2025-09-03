using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public class ChatConversation
	{
		public int Id { get; set; }
		public string Title { get; set; } // خلاصه اولین سوال کاربر
		public DateTime StartedAt { get; set; } = DateTime.Now;
		public DateTime? EndedAt { get; set; }
		// تمام سوالات و پاسخ‌ها در یک فیلد ذخیره می‌شوند
		public string ConversationData { get; set; }
		public  Customer Customer { get; set; }
		public int CustomerId { get; set; }
	}
}
