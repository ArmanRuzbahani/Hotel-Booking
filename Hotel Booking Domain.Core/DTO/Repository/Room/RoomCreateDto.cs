using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entitys_Hotel.Models.HotelEnum;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Room
{
	public class RoomCreateDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public RoomType RoomType { get; set; }
		public int HotelId { get; set; }
		public bool IsEmpty { get; set; }
		public decimal PricePerNight { get; set; }
		public int Discount { get; set; }
	}
}
