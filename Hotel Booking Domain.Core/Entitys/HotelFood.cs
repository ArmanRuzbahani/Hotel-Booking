using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public  class HotelFood
	{
		public int Id { get; set; }

		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }

		public int FoodId { get; set; }
		public Food Food { get; set; }

		public decimal Price { get; set; }    

		public bool IsAvailableInYourHotel { get; set; }

	}
}
