using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.HotelFood
{
	public class HotelFoodUpdateDto
	{
		public int HotelId { get; set; }
		public int FoodId { get; set; }
		public decimal Price { get; set; }
		public bool IsAvailableInYourHotel { get; set; }
	}
}
