using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public class Food
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Picture {  get; set; }

		public List<HotelFood> HotelFoods { get; set; }
	}
}
