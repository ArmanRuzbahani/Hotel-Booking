using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.HotelComments
{
	public class HotelCommentsCreateDto
	{
		public string Content { get; set; }
		public int Rating { get; set; }
		public int HotelId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
