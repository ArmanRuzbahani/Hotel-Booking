using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Entitys_Hotel.Models.HotelEnum;
using Hotel_Booking_Domain.Core.Entitys;

namespace Entitys_Hotel.Models
{
	public class Hotel
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }
		
		public string Description { get; set; }

		public string ShortDescription { get; set; }

		public string OpenAt { get; set; }

		public string CloseAt { get; set; }

		public string Picture { get; set; }

		public IranCityForHotel iranCityForHotel { get; set; }

		public int Stars { get; set; }
		
		public List<HotelAddress> HotelAddresses { get; set; }

		public List<HotelComments> HotelComments { get; set; }

		[ForeignKey("HotelManager")] 
		public int HotelManagerId { get; set; }
		public HotelManager HotelManager { get; set; }

		public List<Room> rooms { get; set; }

		public List<Booking> Bookings { get; set; }

		public  List<HotelFood> hotelFoods { get; set; }

		public List<Facility> Facilities { get; set; }

		public List<HotelRules> HotelRules { get; set; }
	}

}
