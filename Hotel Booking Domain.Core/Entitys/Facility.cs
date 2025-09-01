using Entitys_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.Entitys
{
	public class Facility
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<Hotel> Hotel { get; set; }
	}
}
