using System.ComponentModel.DataAnnotations;

namespace Entitys_Hotel.Models
{
	public class Customer : User
	{
		public List<HotelComments> HotelComments { get; set; }
		public List<Booking> Bookings { get; set; }
		public List<Address> Address { get; set; }
	}
}
