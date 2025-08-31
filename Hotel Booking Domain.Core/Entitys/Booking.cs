namespace Entitys_Hotel.Models
{
	public class Booking
	{
		public int Id { get; set; }

		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }

		public Room Room { get; set; }
		public int RoomId { get; set; }	

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }

		public int NumberOfGuests { get; set; }

		public decimal TotalPrice { get; set; }
		
		public DateTime CreatedDate { get; set; }
		
		public int NumberOfNights { get; set; }

	}
}
