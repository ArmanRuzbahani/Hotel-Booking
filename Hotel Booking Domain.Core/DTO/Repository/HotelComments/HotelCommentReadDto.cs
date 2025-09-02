using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository
{
	public class HotelCommentReadDto
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDateAt { get; set; }
		public int Rating { get; set; }
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
	}
}
