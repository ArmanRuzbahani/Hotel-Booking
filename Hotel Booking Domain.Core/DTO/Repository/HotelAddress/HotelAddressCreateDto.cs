using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.HotelAddress
{
	public class HotelAddressCreateDto
	{
		public string AddressName { get; set; }
		public string Address { get; set; }
		public int HotelId { get; set; }
	}
}
