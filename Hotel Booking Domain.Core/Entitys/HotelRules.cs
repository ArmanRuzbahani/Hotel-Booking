using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public class HotelRules
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Content { get; set; }

		public Hotel Hotel { get; set; }
		public int HotelId { get; set; }
	}
}
