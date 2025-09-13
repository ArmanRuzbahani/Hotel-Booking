using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entitys_Hotel.Models.HotelEnum;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Hotel
{
	public class HotelUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? ShortDescription { get; set; }
		public string? OpenAt { get; set; }
		public string? CloseAt { get; set; }
		public string? Picture { get; set; }
		public IranCityForHotel IranCityForHotel { get; set; }
		public int Stars { get; set; } = 0;
	}
}
