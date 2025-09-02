using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Domain.Core.DTO.Repository.Food
{
	public class FoodForHotelReadDto
	{
		public int Id { get; set; }         
		public string Name { get; set; }    
		public string Description { get; set; } 
		public string Picture { get; set; }  
		public decimal Price { get; set; }  
		public bool IsAvailable { get; set; } 
	}
}
