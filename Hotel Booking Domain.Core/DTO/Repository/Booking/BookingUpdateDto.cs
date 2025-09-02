using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Booking
{
	public class BookingUpdateDto
	{
		public DateTime? CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }
		public int? NumberOfGuests { get; set; }
		public decimal? TotalPrice { get; set; }
		public int? NumberOfNights { get; set; }
	}
}
