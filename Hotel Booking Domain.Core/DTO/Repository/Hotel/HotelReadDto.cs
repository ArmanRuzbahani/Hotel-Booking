using Hotel_Booking_Domain.Core.DTO.HotelAddress;
using Hotel_Booking_Domain.Core.DTO.Repository.Booking;
using Hotel_Booking_Domain.Core.DTO.Repository.Facility;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelFood;
using Hotel_Booking_Domain.Core.DTO.Repository.HotelRules;
using Hotel_Booking_Domain.Core.DTO.Repository.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entitys_Hotel.Models.HotelEnum;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Hotel
{
	public class HotelReadDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string OpenAt { get; set; }
		public string CloseAt { get; set; }
		public string Picture { get; set; }
		public IranCityForHotel IranCityForHotel { get; set; }
		public int Stars { get; set; }
		public int HotelManagerId { get; set; }
		public List<HotelAddressReadDto> HotelAddresses { get; set; }
		public List<HotelCommentReadDto> HotelComments { get; set; }
		public List<RoomReadDto> Rooms { get; set; }
		public List<BookingReadByHotelDto> Bookings { get; set; }
		public List<HotelFoodReadDto> HotelFoods { get; set; }
		public List<FacilityReadDto> Facilities { get; set; }
		public List<HotelRulesReadDto> HotelRules { get; set; }
	}
}
