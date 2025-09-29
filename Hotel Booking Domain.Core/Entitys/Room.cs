using static Entitys_Hotel.Models.HotelEnum;

namespace Entitys_Hotel.Models
{
	public class Room
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public RoomType roomType { get; set; }

		public Hotel Hotel { get; set; }

		public int HotelId { get; set; }

		public bool IsEmpty { get; set; } = true;

		public decimal PricePerNight { get; set; }

		public int Discount { get; set; } // درصد تخفیف

		public decimal CountPriceAfterDiscount { get; set; } // قیمت نهایی بعد از محاسبه تخفیف

		public List<Booking> Bookings { get; set; }

	}
}
