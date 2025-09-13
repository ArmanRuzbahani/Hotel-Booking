using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.HotelRules
{
	public class HotelRulesUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }    
		public string Content { get; set; }
	}
}
